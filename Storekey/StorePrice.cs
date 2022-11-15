namespace Storekey;
using Product_id = System.Int32;
using Volume = System.Int32;
using Price = System.Int32;


public class StorePrice
{

    Dictionary<Product_id, (Volume, Price)> volumeCampaign;
    List<(HashSet<Product_id>, Price)> comboCampaign;
    Dictionary<Product_id, Price> itemPrices;


    public StorePrice(Dictionary<int, (int, int)> volumeCampaign, List<(HashSet<int>, int)> comboCampaign, Dictionary<int, int> itemPrices)
    {
        this.volumeCampaign = volumeCampaign;
        this.comboCampaign = comboCampaign;
        this.itemPrices = itemPrices;
    }

    public Price Campaigner(IEnumerable<Product_id> items)
    {
        Price price = 0;

        // Collects the occurance of the products
        var counted = items.GroupBy(
            x => x,
            (id, itemList) => new CampaignItem { Id = id, Count = itemList.Count() }
        );

        // Adds volume campaign prices to the result
        price += counted.Aggregate(price, (acc, item) =>
            volumeCampaign.ContainsKey(item.Id)
                ? (item.Count / volumeCampaign[item.Id].Item1) * volumeCampaign[item.Id].Item2 + acc
                : acc
        );

        // Removes the volume campaign items
        var volumeReduced = counted.Select(item =>
        {
            if (volumeCampaign.ContainsKey(item.Id))
            {
                var count = (item.Count % volumeCampaign[item.Id].Item1);
                return new CampaignItem { Id = item.Id, Count = count };
            }
            return item;
        });

        // Gather the combo capmpaigns products to one group
        var comboGrouped = volumeReduced.GroupBy(
            x => comboCampaign.FindIndex(campaign => campaign.Item1.Contains(x.Id)),
            (groupIndex, itemList) => new { index = groupIndex, count = itemList.Sum(item => item.Count), elements = itemList }
        );

        // Adds combo campaign prices to the result
        price = comboGrouped.Aggregate(price, (acc, group) =>
            group.index >= 0
                ? (group.count / 2) * comboCampaign[group.index].Item2 + acc
                : acc
        );

        // Removes the combo campaign items
        // if there is odd number of products in one group, the last element will be kept since it can not be paired
        var leftover = comboGrouped.SelectMany(group =>
        {
            if (group.index >= 0)
            {
                if (group.count % 2 > 0)
                {
                    CampaignItem[] odd = { new CampaignItem { Id = group.elements.Last().Id, Count = 1 } };
                    return odd;
                }
                else
                {
                    return new List<CampaignItem>();
                }
            }
            else
            {
                return group.elements;
            }
        });

        return leftover.Aggregate(price, (acc, item) => acc + item.Count * itemPrices[item.Id]);
    }


}

public record CampaignItem
{
    public Product_id Id;
    public int Count;
}
