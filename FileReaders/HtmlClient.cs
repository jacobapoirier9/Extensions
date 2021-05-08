using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Extensions.FileReaders
{
    [Obsolete("Work in progress")]
    public static class Tools
    {
        //public static List<string> CleanSplit(this string str, params char[] splitAt)
        //{
        //    return str.CleanSplit(splitAt.ToList().ConvertAll(item => item.ToString()).ToArray());
        //}

        //public static List<string> CleanSplit(this string str, params string[] splitAt)
        //{
        //    var toReturnString = "";
        //    var temp = "EIOTHISEORHKDSF";
        //    var counter = 0;

        //    foreach (var letter in str)
        //    {
        //        if (counter == 0 && splitAt.Contains(letter.ToString()))
        //        {
        //            toReturnString += temp;
        //        }

        //        toReturnString += letter;

        //        if (splitAt.Contains(letter.ToString()) && ++counter == splitAt.Length)
        //        {
        //            toReturnString += temp;
        //            counter = 0;
        //        }

        //    }

        //    return toReturnString.Split(temp).ToList();
        //}
    }

    public class HtmlClient
    {
        private static string _basePath = @"C:\Users\poirier\Documents\TestFiles";

        public static List<string> GetLines()
        {
            var which = "Test";

            _basePath = Path.Combine(_basePath, which);

            var text = File.ReadAllText(Path.Combine(_basePath, "index.html"));

            var html = text.Split('<', '>').ToList();
            html.RemoveAll(line => line.ContainsAny("\r\n", "\n", "\t") || line == "");

            return html;
        }

        public static void Run()
        {
            var html = GetLines();
            var cursor = 0;

            using (var writer = new StreamWriter(Path.Combine(_basePath, "Cursor.txt")))
            {
                foreach (var line in html.ReplaceAll("  ", " "))
                {
                    switch (line.GetTagType())
                    {
                        case HtmlTagType.Opening:
                            writer.WriteLine($"{line} - {line.GetTagType()} {cursor++}");
                            break;

                        case HtmlTagType.Closing:
                            writer.WriteLine($"{line} - {line.GetTagType()} {--cursor}");
                            break;

                        case HtmlTagType.SelfClosing:
                            writer.WriteLine($"{line} - {line.GetTagType()} {cursor}");
                            break;

                        default:
                            writer.WriteLine($"{line,-20} - {line.GetTagType()} {cursor}");
                            break;
                    }
                }
            }

            cursor = 0;
            var repeatChar = "  ";

            using (var openWriter = new StreamWriter(Path.Combine(_basePath, "Open.txt")))
            using (var closWriter = new StreamWriter(Path.Combine(_basePath, "Clos.txt")))
            using (var selfWriter = new StreamWriter(Path.Combine(_basePath, "Self.txt")))
            using (var textWriter = new StreamWriter(Path.Combine(_basePath, "Text.txt")))
            using (var allWriter = new StreamWriter(Path.Combine(_basePath, "All.txt")))
            {
                foreach (var line in html.ReplaceAll("  ", " "))
                {
                    switch (line.GetTagType())
                    {
                        case HtmlTagType.Opening:
                            openWriter.WriteLine($"<{line}>");
                            allWriter.WriteLine($"{repeatChar.Repeat(cursor)}{line} - {line.GetTagType()}");
                            cursor++;
                            break;

                        case HtmlTagType.Closing:
                            closWriter.WriteLine($"<{line}>");
                            --cursor;
                            allWriter.WriteLine($"{repeatChar.Repeat(cursor)}{line} - {line.GetTagType()}");
                            break;

                        case HtmlTagType.SelfClosing:
                            selfWriter.WriteLine($"<{line}>");
                            allWriter.WriteLine($"{repeatChar.Repeat(cursor)}{line} - {line.GetTagType()}");
                            break;

                        default:
                            textWriter.WriteLine($"{line}");
                            allWriter.WriteLine($"{repeatChar.Repeat(cursor)}{line} - {line.GetTagType()}");
                            break;
                    }
                }
            }



            // Find tag count
            html.RemoveAll(line => line.GetTagType() == HtmlTagType.Text || line.GetTagType() == HtmlTagType.SelfClosing);
            html.Sort();

            var tags = html.Select(line => line.Split(' ')[0]);
            var data = new Dictionary<string, int>();

            var reportOnTags = new List<string>();

            foreach (var tag in tags)
            {
                if (data.ContainsKey(tag))
                {
                    data[tag] += 1;
                }
                else
                {
                    data.Add(tag, 1);
                }
            }

            foreach (var keyvalue in data.Where(line => !line.Key.Contains("/")))
            {
                var opening = keyvalue.Value;
                int closing;
                try
                {
                    closing = data[$"/{keyvalue.Key}"];
                }
                catch
                {
                    closing = 0;
                }

                Console.WriteLine($"{keyvalue.Key,10} - {opening,10} Opening, {closing,10} Closing");

                if (opening != closing)
                {
                    reportOnTags.Add(keyvalue.Key);
                    Console.WriteLine($"-".Repeat(60));
                }
            }

            reportOnTags.PrintAll();
            foreach (var reportOnTag in reportOnTags)
            {
                using (var tagWriter = new StreamWriter(Path.Combine(_basePath, $@"Tags\{reportOnTag}.txt")))
                {
                    foreach (var line in html.ReplaceAll("  ", " ")
                                             .Select(line => line.Split(' ')[0]).ToList()
                                             .FindAll(l => l.StartsWith(reportOnTag) ||
                                                           l.StartsWith($"/{reportOnTag}")))
                    {
                        tagWriter.WriteLine($"{repeatChar.Repeat(cursor)}{line}");
                        switch (line.GetTagType())
                        {
                            case HtmlTagType.Opening:
                                tagWriter.WriteLine($"{line}");
                                break;

                            case HtmlTagType.Closing:
                                tagWriter.WriteLine($"{line}");
                                break;
                        }
                    }
                }

            }

            var opened = tags.Where(t => t.GetTagType() == HtmlTagType.Opening);
            var closed = tags.Where(t => t.GetTagType() == HtmlTagType.Closing);

            Console.WriteLine($"Opening {opened.Count()}");
            Console.WriteLine($"Closing {closed.Count()}");

            Console.WriteLine($"Off by {cursor}");
        }
    }



    public static class Tests
    {
        private static List<string> tags = new List<string>()
        {
            "a", "abbr", "acronym", "address", "applet", "area", "article", "aside", "audio", "b", "base", "basefont", "bdi",
            "bdo", "big", "blockquote", "body", "br", "button", "canvas", "caption", "center", "cite", "code", "col", "colgroup",
            "data", "datalist", "dd", "del", "details", "dfn", "dialog", "dir", "div", "dl", "dt", "em", "embed", "fieldset", "figcaption",
            "figure", "font", "footer", "form", "frame", "frameset", "h1", "h2", "h3", "h4", "h5", "h6", "h7", "head", "header", "hr",
            "html", "i", "iframe", "img", "input", "ins", "kbd", "label", "legend", "li", "link", "main", "map", "mark", "meta", "meter",
            "nav", "noscript", "object", "ol", "optgroup", "option", "output", "p", "path", "param", "picture", "pre", "progress", "q", "rp",
            "rt", "ruby", "s", "samp", "script", "section", "select", "small", "source", "span", "strike", "strong", "style",
            "sub", "summary", "sup", "svg", "table", "tbody", "td", "template", "textarea", "tfoot", "th", "thead", "time", "title",
            "tr", "track", "tt", "u", "ul", "use", "video", "wbr"
        };


        private static List<string> selfClosingTags = new List<string>()
        {
            "meta", "link", "input", "br", "hr", "area", "base", "col", "embed", "param", "source", "track", "wbr", "img"
        };

        public static HtmlTagType GetTagType(this string str)
        {
            str = str.TrimStart('<').TrimEnd('>');
            var strSplit = str.Split(' ');

            if (tags.Contains(strSplit[0]) && strSplit[strSplit.Length - 1].Contains(" /") || selfClosingTags.Contains(strSplit[0]))
                return HtmlTagType.SelfClosing;

            else if (strSplit[0].StartsWith("/") && tags.Contains(strSplit[0].Replace("/", "")))
                return HtmlTagType.Closing;

            else if (tags.Contains(strSplit[0]))
                return HtmlTagType.Opening;

            else
                return HtmlTagType.Text;
        }
    }

    public enum HtmlTagType : byte
    {
        /// <summary>
        /// Used for any text in an html file
        /// </summary>
        Text = 0,

        /// <summary>
        /// Used for a tag that indicates an opening tag
        /// </summary>
        Opening = 1,

        /// <summary>
        /// Used for a tag that indicates a closing tag
        /// </summary>
        Closing = 2,

        /// <summary>
        /// Used for a tag that indicates a self closing tag
        /// </summary>
        SelfClosing = 3
    }
}
