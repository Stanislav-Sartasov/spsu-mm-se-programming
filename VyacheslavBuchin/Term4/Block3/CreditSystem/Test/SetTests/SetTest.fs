namespace Test.SetTests

open System
open System.Threading
open ConcurrentSet
open NUnit.Framework

[<AbstractClass>]
type SetTest() =
    abstract member Set : ISet<int>

    [<Test>]
    [<TestCase(1)>]
    [<TestCase(4)>]
    [<TestCase(8)>]
    member test.``all unique items should be added`` threadCnt =
        let r = Random(42)
        let size = 1000
        let items = Array.init size id |> Array.sortBy (fun _ -> r.Next())
        let set = test.Set
        let threads =
            Array.splitInto threadCnt items
            |> Array.map (fun xs -> Thread(fun () -> Array.iter (set.Add >> ignore) xs))

        threads |> Array.iter (fun t -> t.Start())
        threads |> Array.iter (fun t -> t.Join())

        Array.forall set.Contains items |> Assert.IsTrue
        Assert.IsTrue (set.Count() = size)

    [<Test>]
    member test.``removing not existing item should return false`` () =
        let set = test.Set
        let item = 42

        Assert.IsFalse <| set.Contains item
        Assert.IsFalse <| set.Remove item

    [<Test>]
    member test.``after adding item set should contain it`` () =
        let set = test.Set
        let item = 42

        Assert.IsTrue <| set.Add item
        Assert.IsTrue <| set.Contains item

    [<Test>]
    [<TestCase(1)>]
    [<TestCase(10)>]
    [<TestCase(100)>]
    member test.``adding item more than once should not affect the set`` addingCnt =
        let set = test.Set
        let item = 42

        for i = 1 to addingCnt do set.Add item |> ignore

        Assert.IsTrue <| set.Contains item
        set.Count() = 1 |> Assert.IsTrue
