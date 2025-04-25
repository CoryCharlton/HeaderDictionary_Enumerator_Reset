//#define USE_FIX // Comment this line to use the original ASP.NET Core HeaderDictionary

using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

#if USE_FIX
var headerDictionary = new FixedHeaderDictionary
{
    {"key1", "value1"},
    {"key2", "value2"},
    {"key3", "value3"}
};
#else
var headerDictionary = new HeaderDictionary
{
    {"key1", "value1"},
    {"key2", "value2"},
    {"key3", "value3"}
};
#endif


var enumerator = headerDictionary.GetEnumerator();
var initial = enumerator.Current;

var moved = enumerator.MoveNext();
Debug.Assert(moved);

var first = enumerator.Current;

Console.WriteLine($"initial != first: {!initial.Equals(first)} - initial: {initial} - first: {first}");

var last = enumerator.Current;

while (enumerator.MoveNext())
{
    last = enumerator.Current;    
}

Console.WriteLine($"last != first: {!last.Equals(first)} - last: {last} - first: {first}");

var ienumerator = (IEnumerator)enumerator;

ienumerator.Reset();

#if USE_FIX
enumerator = (FixedHeaderDictionary.Enumerator)ienumerator;
#else
enumerator = (HeaderDictionary.Enumerator)ienumerator;
#endif

moved = enumerator.MoveNext();

Console.WriteLine($"enumerator.Current.Equals(first): {enumerator.Current.Equals(first)} - enumerator.Current: {enumerator.Current} - first: {first} - moved: {moved}");
Debug.Assert(moved);

Console.ReadKey();