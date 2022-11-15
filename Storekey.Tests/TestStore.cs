namespace Storekey.Tests;
using Xunit;
using Product_id = System.Int32;
using Volume = System.Int32;
using Price = System.Int32;

public class TestStore
{
    public static Dictionary<Product_id, (Volume, Price)> volumeCampaign = new Dictionary<Product_id, (Volume, Price)>{
        {1,(2,10)},
        {2,(4,15)},
        {4,(3,20)}

    };
    public static List<(HashSet<Product_id>, Price)> comboCampaign = new List<(HashSet<int>, Price)>{
        (new HashSet<Product_id>{5,6,7},10),
        (new HashSet<Product_id>{8,12},20),
        (new HashSet<Product_id>{2,11},20),
        (new HashSet<Product_id>{9,10},25),
    };

    public static Dictionary<Product_id, Price> itemPrices = new Dictionary<Product_id, Price>{
        {1,10},
        {2,17},
        {3,17},
        {4,18},
        {5,19},
        {6,20},
        {7,21},
        {8,22},
        {9,23},
        {10,17},
        {11,17},
        {12,17},
        {13,17},
        {14,17},
        {15,17},
        {16,15},
        {17,15},
    };

    public static StorePrice sp = new StorePrice(volumeCampaign, comboCampaign, itemPrices);

    [Fact]
    public void NoCampingTest()
    {
        var expectedResult = 30;
        var itemlist = new List<Product_id> { 16, 17 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void VolumeBasicTest()
    {
        var expectedResult = 10;
        var itemlist = new List<Product_id> { 1, 1 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void VolumeMoreTest()
    {
        var expectedResult = 20;
        var itemlist = new List<Product_id> { 1, 1, 1 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ComboBasicTest()
    {
        var expectedResult = 25;
        var itemlist = new List<Product_id> { 7, 6, 17 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ComboMoreTest()
    {
        var expectedResult = 31;
        var itemlist = new List<Product_id> { 5, 6, 7 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void VolComboBasicTest()
    {
        var expectedResult = 50;
        var itemlist = new List<Product_id> { 2, 12, 2, 8, 16, 2, 2 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void VolComboTest()
    {
        var expectedResult = 67;
        var itemlist = new List<Product_id> { 11, 2, 12, 2, 8, 16, 2, 2 };

        var result = sp.Campaigner(itemlist);

        Assert.Equal(expectedResult, result);
    }
}