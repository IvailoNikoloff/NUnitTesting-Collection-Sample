namespace Collection.UnitTests;

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
        
    }

    [Test]
    public void Test_Collection_1MillionItems()
    {
        
    }

}