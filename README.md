# RegexUp
Compose, decompose and analyze regular expressions.

## Summary
This is an experimental project aimed at converting regular expressions into an object model and back again. An object model gives you the ability to inspect a regular expression as a series of structured objects, allowing you to easily inspect and manipulate them in order to compose and generate new regular expressions. Another advantage is objects can help to prevent invalid regular expressions through strict interfaces and argument checking.

A future aim of this project is to systematically execute each sub-expression against an input string. This would essentially allow you to "debug" a regex to see where it is failing.

## Quick Reference
Regular expressions are composed of a wide variety of special sequences and characters. While an regex expert might know the names for these terms, I do not expect the average developer to be familiar with them. For that reason, I will quickly list each class found in RegexUp along with some examples:

* **Literal** - `a`, `b`, `c`
* **Alternation** - `a|b|c`
* **CaptureGroup** - `(abc)`, `(?<name>abc)`, `(?'name'a)`
* **CharacterGroup** - `[abc]`, `[^a-zA-Z0-9]`
* **Quantifiers** - `[0-9]+`, `.*`, `(abc)?`, `a{0,5}`
* **Anchor** - `^`, `$`, `\b`, `\G`
* **Escape** - `\t`, `\n`, `\u0037`
* **CharacterClass** - `\w`, `\s`, `\p{IsBasicLatin}`, `\P{Lu}`
* **Backreference** - `(abc).*\1`, `(?<name>abc).*\k<name>`
* **NonCaptureGroup** - `(?:abc)`
* **OptionsGroup** - `(?im-nx:abc)`
* **BalancedGroup** - `((?'Open'\()[^)]*)+((?'Close-Open')[^()]*)+`
* **PositiveLookaheadAssertion** - `a(?=bc)`
* **NegativeLookaheadAssertion** - `abc(?!d)`
* **PositiveLookbehindAssertion** - `(?<=a)bc`
* **NegativeLookbehindAssertion** - `(?<!a)bc`
* **NonbacktrackingAssertion** - `a(?>bc)?bc`


## Quick Example
Regular expressions are a very compact notation. Building the same regular expressions using classes and method calls is significantly more verbose. Furthermore, manipulating objects to generate an otherwise static regular expression requires runtime overhead that's unnecessary. As such, one use case for RegexUp is to generate regular expressions in a test project and then simply copy the generated regular expression string to your production code. Another use case is if you need to build your regex at runtime based on user input.

Please see this [example](https://github.com/jehugaleahsa/RegexUp/blob/master/RegexUp.Tests/RealWorldTester.cs#L9) unit test that constructs a regular expression for checking for valid URLs.

## Usage
As this project is currently only experimental, I have not made it consumable via NuGet. In order to use this code, you must copy the source code into your project. If you have a use for this project and would like to experiment, please let me know and I will publish it to NuGet upon request.

## License
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>