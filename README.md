# csharp-inconsistent-warning-ca1062

This repository is related to [issue 1416138](https://developercommunity2.visualstudio.com/t/Field-assignment-from-reference-type-arg/1416138).

Assigning an argument specified as a reference type (e.g. `string`) to an instance field
(both in the constructor and in a plain method) does not trigger a CA1062 warning about
a potential **nullability issue**.

The compiler only generates these two warnings:

> warning CA1062: In externally visible method 'Foo.Foo(string x)', validate parameter 'x' is non-null before using it.
> If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.
>
> warning CA1062: In externally visible method 'void Foo.SetX(string x)', validate parameter 'x' is non-null before using it.
> If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.

```csharp
    public class Foo
    {
        public Foo(string x)
        {
            this.x = x; // does not warn
            this.len = x.Length; // warns (OK)
        }

        public void SetX(string x)
        {
            this.x = x; // does not warn
            this.len = x.Length; // warns (OK)
        }

        public string X => this.x;
        public int Len => this.len;

        private string x;
        private int len;
    }
```

I am expecting that if I assign `x` to `this.x` without checking that `x` is indeed **not null**,
then I have the risk of using `this.x` in cases where it may not be null.

## Context

The warnings are generated when following settings are used:

```csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net5.0</TargetFramework>
    <LangVersion>latest</LangVersion>
    <Nullable>enable</Nullable>
    <EnableNETAnalyzers>true</EnableNETAnalyzers> <!-- enable analyzers -->
    <AnalysisMode>AllEnabledByDefault</AnalysisMode> <!-- enable ALL analyzers -->
  </PropertyGroup>
</Project>
```
