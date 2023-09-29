Imports System.Data

Module Program
    Sub Main(args As String())

        Dim dt As New DataTable("tblEntTable")
        dt.Columns.Add("ID", GetType(String))
        dt.Columns.Add("amount", GetType(Decimal))
        dt.Rows.Add(New Object() {"1", 100.51})
        dt.Rows.Add(New Object() {"1", 200.52})
        dt.Rows.Add(New Object() {"2", 500.24})
        dt.Rows.Add(New Object() {"2", 400.31})
        dt.Rows.Add(New Object() {"3", 600.88})
        dt.Rows.Add(New Object() {"3", 700.11})

        Dim result = (From orders In dt.AsEnumerable
                      Group orders By ID = orders.Field(Of String)("ID") Into g = Group
                      Select New With {Key ID,
            .Amount = g.Sum(Function(r) r.Field(Of Decimal)("amount"))
        }).OrderBy(Function(tkey) tkey.ID).ToList()

        Console.WriteLine($"ID{vbTab}Amount")
        For index = 0 To result.Count - 1
            Console.WriteLine($"{result(index).ID}{vbTab}{result(index).Amount.ToString("N")}")
        Next

        Console.ReadLine()
    End Sub
End Module
