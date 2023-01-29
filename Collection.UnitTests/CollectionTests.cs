

using System;
using System.Linq;
using Microsoft.VisualBasic;

public class CollectionTests
{  

    [Test]
    public void Test_Collection_EmptyConstructor()
    {
        var coll = new Collection<int>();
        
        Assert.AreEqual(coll.ToString(), "[]");
    }

    [Test]
    public void Test_Collection_ConstructorSingleItem()
    {
        var coll = new Collection<int>(5);
        var expected = "[5]";
        
        Assert.AreEqual(coll.ToString(), expected);
    }

    [Test]
    public void Test_Collection_ConstructorMultipleItems()
    {
        var coll = new Collection<int>(5, 6);
        var expected = "[5, 6]";
        
        Assert.AreEqual(coll.ToString(), expected);
    }

    [Test]
    public void Test_Collection_Add()
    {
        var coll = new Collection<string>("Ivan", "Petar");
        var expected = "[Ivan, Petar, Gosho]";
        
        coll.Add("Gosho");
        
        Assert.AreEqual(coll.ToString(), expected);
    }

    [Test]
    public void Test_Collection_AddWithGrow()
    {
        var nums = new Collection<int>();
        int oldCapacity = nums.Capacity;
        var newNums = Enumerable.Range(1000, 2000).ToArray();
        
        nums.AddRange(newNums);

        string expectedNums = "[" + string.Join(", ", newNums) + "]";
        Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
        Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
        Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

    }

    [Test]
    public void Test_Collection_AddRange()
    {
        
    }

    [Test]
    public void Test_Collection_GetByIndex()
    {
        var coll = new Collection<int>(5, 6, 7);
        var item = coll[1];
        
        Assert.That(item.ToString(), Is.EqualTo("6"));
    }

    [Test]
    public void Test_Collection_GetByInvalidIndexInRange()
    {
        var coll = new Collection<int>(5, 6, 7);
        var item = coll[1];
        
        Assert.That(item.ToString(), Is.Not.EqualTo("5"));
    }
    
    // DDT
    [TestCase ("5, 6, 7", 1, "6")]
    [TestCase ("Pesho, Gosho, Ivan", 2, "Ivan")]
    [TestCase ("Pesho, Gosho, Ivan", 2, "Ivan")]
    public void Test_Collection_GetByValidIndexInRange_DDT_Approach(string data, int index, string expected)
    {
        var coll = new Collection<string>(data.Split(", "));
        var item = coll[index];
        
        Assert.That(item.ToString(), Is.EqualTo(expected));
    }
    
    
    [Test]
    public void Test_Collection_GetByInvalidIndexOutOfRange()
    {
        var coll = new Collection<int>(5, 6, 7);
        
        Assert.That(() => { var colls = coll[-1]; },
            Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(() => { var colls = coll[3]; },
            Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(() => { var colls = coll[500]; },
            Throws.InstanceOf<ArgumentOutOfRangeException>());
        Assert.That(coll.ToString(), Is.EqualTo("[5, 6, 7]"));

    }

    [Test]
    public void Test_Collection_SetByIndex()
    {
        var coll = new Collection<int>(5, 6, 7);
        coll[1] = 666;
        
        Assert.That(coll.ToString(), Is.EqualTo("[5, 666, 7]"));
    }

    [Test]
    public void Test_Collection_SetByInvalidIndex()
    {
        var coll = new Collection<int>(5, 6, 7);

        Assert.That(() =>
        { coll[25] = 666;}, Throws.InstanceOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Test_Collection_AddRangeWithGrow()
    {
        
    }

    [Test]
    public void Test_Collection_InsertAtStart()
    {
        var coll = new Collection<int>(5, 6, 7);
        
        coll.InsertAt(0, 55);
        
        Assert.That(coll.ToString(), Is.EqualTo("[55, 5, 6, 7]"));
    }

    [Test]
    public void Test_Collection_InsertAtEnd()
    {
        var coll = new Collection<int>(5, 6, 7);
        
        coll.InsertAt(coll.Count, 55);
        
        Assert.That(coll.ToString(), Is.EqualTo("[5, 6, 7, 55]"));
    }

    [Test]
    public void Test_Collection_InsertAtMiddle()
    {
        var coll = new Collection<int>(5, 6, 7, 8);
        
        coll.InsertAt(coll.Count / 2, 55);
        
        Assert.That(coll.ToString(), Is.EqualTo("[5, 6, 55, 7, 8]"));
    }

    [Test]
    public void Test_Collection_InsertAtWithGrow()
    {
        var nums = new Collection<int>();
        int oldCapacity = nums.Capacity;
        var newNums = Enumerable.Range(1000, 2000).ToArray();

        for (int i = 0; i < newNums.Length; i++)
        {
            nums.InsertAt(i, newNums[i]);
        }
        

        string expectedNums = "[" + string.Join(", ", newNums) + "]";
        Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
        Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
        Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
    }

    [Test]
    public void Test_Collection_InsertAtInvalidIndex()
    {
        var coll = new Collection<int>(5, 6, 7, 8);

        var indexOutOfRange = coll.Capacity + 1;
        
        Assert.That(() => {coll.InsertAt(indexOutOfRange, 55);},
            Throws.InstanceOf<ArgumentOutOfRangeException>());

    }

    [Test]
    public void Test_Collection_ExchangeMiddle()
    {
        var coll = new Collection<int>(5, 6, 7, 8);
        
        coll.Exchange((coll.Count / 2) -1, (coll.Count / 2));
        
        Assert.That(coll.ToString(), Is.EqualTo("[5, 7, 6, 8]"));
    }

    [Test]
    public void Test_Collection_ExchangeFirstLast()
    {
        var coll = new Collection<int>(5, 6, 7, 8);

        var first = 0;
        var last = coll.Count - 1;
        
        coll.Exchange(first, last);
        
        Assert.That(coll.ToString(), Is.EqualTo("[8, 6, 7, 5]"));
    }

    [Test]
    public void Test_Collection_ExchangeInvalidIndexes()
    {
        
    }

    [Test]
    public void Test_Collection_RemoveAtStart()
    {
        var coll = new Collection<int>(5, 6, 7, 8);

        
        
        coll.RemoveAt(0);
        
        Assert.That(coll.ToString(), Is.EqualTo("[6, 7, 8]"));
    }

    [Test]
    public void Test_Collection_RemoveAtEnd()
    {
        var coll = new Collection<int>(5, 6, 7, 8);

        var end = coll.Count - 1;
        
        coll.RemoveAt(end);
        
        Assert.That(coll.ToString(), Is.EqualTo("[5, 6, 7]"));
    }

    [Test]
    public void Test_Collection_RemoveAtMiddle()
    {
        
    }

    [Test]
    public void Test_Collection_RemoveAtInvalidIndex()
    {
        
    }

    [Test]
    public void Test_Collection_RemoveAll()
    {
        
    }

    [Test]
    public void Test_Collection_Clear()
    {
        
    }

    [Test]
    public void Test_Collection_CountAndCapacity()
    {
        var coll = new Collection<int>(5, 6);

        var checkCapacity = Math.Max(2 * coll.Count, 16);
        
        Assert.AreEqual(coll.Count, 2);
        Assert.That(coll.Capacity, Is.EqualTo(checkCapacity));
    }

    [Test]
    public void Test_Collection_ToStringEmpty()
    {
        var coll = new Collection<int>(5, 6, 7, 8);
        coll.Clear();

        Assert.AreEqual(coll.ToString(), "[]");

    }

    [Test]
    public void Test_Collection_ToStringSingle()
    {
        Assert.Pass();
    }

    [Test]
    public void Test_Collection_ToStringMultiple()
    {
        
    }

    [Test]
    public void Test_Collection_ToStringNestedCollections()
    {
        var names = new Collection<string>("Teddy", "Gerry");
        var nums = new Collection<int>(10, 20);
        var dates = new Collection<DateTime>();
        var nested = new Collection<object>(names, nums, dates);
        string nestedToString = nested.ToString();
        Assert.That(nestedToString, 
            Is.EqualTo("[[Teddy, Gerry], [10, 20], []]"));

    }
    
    //DDT
    [TestCase("Teddy, Gerry", "10, 20", "01-01-2020", "[[Teddy, Gerry], [10, 20], [1.1.2020 г. 0:00:00]]")]
    [TestCase("Teddy, Gerry, Pesho", "2000", "01-01-2020, 01-02-2023", "[[Teddy, Gerry, Pesho], [2000], [1.1.2020 г. 0:00:00, 1.2.2023 г. 0:00:00]]")]
    public void Test_Collection_ToStringNestedCollections_DDT_Approach(string names, string numbers, string dates , string expected)
    {
        
        var namesColl = new Collection<string>(names.Split(", "));

        var numsCollRaw = new Collection<string>(numbers.Split(", "));
        var numbersColl = new Collection<int>();
        for (int i = 0; i < numsCollRaw.Count; i++)
        {
            numbersColl.Add(int.Parse(numsCollRaw[i]));
        }
        


        var datesRaw = new Collection<string>(dates.Split(", "));
        var datesColl = new Collection<DateTime>();
        for (int i = 0; i < datesRaw.Count; i++)
        {
            datesColl.Add(DateTime.Parse(datesRaw[i]));
        }
        
        var nested = new Collection<object>(namesColl, numbersColl, datesColl);
        string nestedToString = nested.ToString();
        Assert.That(nestedToString, 
            Is.EqualTo(expected));

    }
    
    

    [Test]
    public void Test_Collection_1MillionItems()
    {
        const int itemsCount = 1000000;
        var nums = new Collection<int>();
        nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
        Assert.That(nums.Count == itemsCount);
        Assert.That(nums.Capacity >= nums.Count);
        for (int i = itemsCount - 1; i >= 0; i--)
            nums.RemoveAt(i);
        Assert.That(nums.ToString() == "[]");
        Assert.That(nums.Capacity >= nums.Count);

    }

}