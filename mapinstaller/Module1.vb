Imports System.IO
Module Module1
    Function spacer() As String
        Dim s As String = ""
        s = " "
        For i = 3 To Console.WindowWidth
            s += "-"
        Next
        Return s
    End Function

    Function center(text As String) As String
        Dim s As String = ""
        For i = 1 To (Console.WindowWidth - text.Length) / 2
            s += " "
        Next
        s += text
        Return s
    End Function

    Sub WriteLineColor(text As String, color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(text)
        Console.ResetColor()
    End Sub

    Sub Main()
        Dim localAppData As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        Dim plutoPath As String = localAppData + "\Plutonium\storage\iw5\"

        Console.Title = "IW5 map installer"
        WriteLineColor(vbNewLine + center("Plutonium IW5 map installer by mxve") + vbNewLine, ConsoleColor.DarkMagenta)

        For Each d As String In Directory.GetDirectories("maps")
            WriteLineColor(spacer, ConsoleColor.DarkGray)
            WriteLineColor(vbNewLine + center(d.Split("\")(1)) + vbNewLine, ConsoleColor.Yellow)

            If File.Exists(d + "\readme.txt") Then
                Dim readme As String() = File.ReadAllLines(d + "\readme.txt")
                For Each s In readme
                    Console.WriteLine("   " + s)
                Next
            End If

            WriteLineColor(vbNewLine + spacer() + vbNewLine, ConsoleColor.DarkGray)

            For Each f As String In Directory.GetFiles(d)
                Dim dir As String = ""

                If f.EndsWith(".arena") Then
                    dir = "mp\"
                ElseIf f.EndsWith(".ff") Then
                    dir = "zone\"
                ElseIf f.EndsWith(".iwd") Then
                    dir = ""
                Else
                    Continue For
                End If

                Console.WriteLine("  - " + f + " -> " + plutoPath + dir)

                Try
                    If File.Exists(plutoPath + dir + f.Split("\")(f.Split("\").Length - 1)) Then File.Delete(plutoPath + dir + f.Split("\")(f.Split("\").Length - 1))
                    File.Copy(f, plutoPath + dir + f.Split("\")(f.Split("\").Length - 1))
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                End Try
            Next

            WriteLineColor(vbNewLine + spacer() + vbNewLine + vbNewLine, ConsoleColor.DarkGray)
        Next

        WriteLineColor(vbNewLine + "   Done. Press Enter to Exit..", ConsoleColor.Green)
        Console.ReadLine()
    End Sub
End Module