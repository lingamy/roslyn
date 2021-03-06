' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.CodeAnalysis
Imports Microsoft.VisualStudio.LanguageServices.CSharp.CodeModel.Extenders
Imports Microsoft.VisualStudio.LanguageServices.CSharp.CodeModel.Interop
Imports Roslyn.Test.Utilities

Namespace Microsoft.VisualStudio.LanguageServices.UnitTests.CodeModel.CSharp
    Public Class CodeFunctionTests
        Inherits AbstractCodeFunctionTests

#Region "Access tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access1()
            Dim code =
<Code>
class C
{
    int $$F() { throw new System.NotImplementedException(); }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPrivate)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access2()
            Dim code =
<Code>
class C
{
    private int $$F() { throw new System.NotImplementedException(); }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPrivate)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access3()
            Dim code =
<Code>
class C
{
    protected int $$F() { throw new System.NotImplementedException(); }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProtected)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access4()
            Dim code =
<Code>
class C
{
    protected internal int $$F() { throw new System.NotImplementedException(); }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProjectOrProtected)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access5()
            Dim code =
<Code>
class C
{
    internal int $$F() { throw new System.NotImplementedException(); }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessProject)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access6()
            Dim code =
<Code>
class C
{
    public int $$F() { throw new System.NotImplementedException(); }
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPublic)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Access7()
            Dim code =
<Code>
interface I
{
    int $$Foo();
}
</Code>

            TestAccess(code, EnvDTE.vsCMAccess.vsCMAccessPublic)
        End Sub

#End Region

#Region "CanOverride tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub CanOverride1()
            Dim code =
<Code>
abstract class C
{
    protected abstract void $$Foo();
}
</Code>

            TestCanOverride(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub CanOverride2()
            Dim code =
<Code>
interface I
{
    void $$Foo();
}
</Code>

            TestCanOverride(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub CanOverride3()
            Dim code =
<Code>
class C
{
    protected virtual void $$Foo() { }
}
</Code>

            TestCanOverride(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub CanOverride4()
            Dim code =
<Code>
class C
{
    protected void $$Foo() { }
}
</Code>

            TestCanOverride(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub CanOverride5()
            Dim code =
<Code>
class B
{
    protected virtual void Foo()
    {
    }
}

class C : B
{
    protected override void $$Foo()
    {
        base.Foo();
    }
}
</Code>

            TestCanOverride(code, False)
        End Sub

#End Region

#Region "FullName tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub FullName_Destructor()
            Dim code =
<Code>
class C
{
    ~C$$() { }
}
</Code>

            TestFullName(code, "C.~C")
        End Sub

#End Region

#Region "FunctionKind tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub FunctionKind_Destructor()
            Dim code =
<Code>
class C
{
    ~C$$() { }
}
</Code>

            TestFunctionKind(code, EnvDTE.vsCMFunction.vsCMFunctionDestructor)
        End Sub

#End Region

#Region "MustImplement tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub MustImplement1()
            Dim code =
<Code>
abstract class C
{
    protected abstract void $$Foo();
}
</Code>

            TestMustImplement(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub MustImplement2()
            Dim code =
<Code>
interface I
{
    void $$Foo();
}
</Code>

            TestMustImplement(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub MustImplement3()
            Dim code =
<Code>
class C
{
    protected virtual void $$Foo() { }
}
</Code>

            TestMustImplement(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub MustImplement4()
            Dim code =
<Code>
class C
{
    protected void $$Foo() { }
}
</Code>

            TestMustImplement(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub MustImplement5()
            Dim code =
<Code>
class B
{
    protected virtual void Foo()
    {
    }
}

class C : B
{
    protected override void $$Foo()
    {
        base.Foo();
    }
}
</Code>

            TestMustImplement(code, False)
        End Sub

#End Region

#Region "Name tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Name_Destructor()
            Dim code =
<Code>
class C
{
    ~C$$() { }
}
</Code>

            TestName(code, "~C")
        End Sub

#End Region

#Region "OverrideKind tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub OverrideKind_Abstract()
            Dim code =
<Code>
abstract class C
{
    protected abstract void $$Foo();
}
</Code>

            TestOverrideKind(code, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindAbstract)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub OverrideKind_Virtual()
            Dim code =
<Code>
class C
{
    protected virtual void $$Foo() { }
}
</Code>

            TestOverrideKind(code, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindVirtual)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub OverrideKind_Sealed()
            Dim code =
<Code>
class C
{
    protected sealed void $$Foo() { }
}
</Code>

            TestOverrideKind(code, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindSealed)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub OverrideKind_Override()
            Dim code =
<Code>
abstract class B
{
    protected abstract void Foo();
}

class C : B
{
    protected override void $$Foo() { }
}
</Code>

            TestOverrideKind(code, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindOverride)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub OverrideKind_New()
            Dim code =
<Code>
abstract class B
{
    protected void Foo();
}

class C : B
{
    protected new void $$Foo() { }
}
</Code>

            TestOverrideKind(code, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindNew)
        End Sub

#End Region

#Region "Prototype tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_FullNameOnly()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeFullname, "A.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_UniqueSignature()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeUniqueSignature, "A.MethodC(int,bool)")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ParamTypesOnly()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeParamTypes, "MethodC (int, bool)")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ParamNamesOnly()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeParamNames, "MethodC (intA, boolB)")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ReturnType()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeType, "bool MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ClassName1()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "A.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ClassName2()
            Dim code =
<Code>
class A&lt;T&gt;
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "A<>.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ClassName3()
            Dim code =
<Code>
class C&lt;T&gt;
{
    class A
    {
        internal static bool $$MethodC(int intA, bool boolB)
        {
            return boolB;
        }
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "C<>.A.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ClassName4()
            Dim code =
<Code>
class C
{
    class A&lt;T&gt;
    {
        internal static bool $$MethodC(int intA, bool boolB)
        {
            return boolB;
        }
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "C.A<>.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ClassName5()
            Dim code =
<Code>
class C
{
    class A
    {
        internal static bool $$MethodC(int intA, bool boolB)
        {
            return boolB;
        }
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "C.A.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_ClassName6()
            Dim code =
<Code>
namespace N
{
    class A
    {
        internal static bool $$MethodC(int intA, bool boolB)
        {
            return boolB;
        }
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "A.MethodC")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Constructor_Unique()
            Dim code =
<Code>
class A
{
    public $$A()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeUniqueSignature, "A.#ctor()")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Finalizer_Unique()
            Dim code =
<Code>
class A
{
    ~A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeUniqueSignature, "A.#dtor()")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Unique_InvalidCombination()
            Dim code =
<Code>
class A
{
    internal static bool $$MethodC(int intA, bool boolB)
    {
        return boolB;
    }
}
</Code>

            TestPrototypeThrows(Of ArgumentException)(code, EnvDTE.vsCMPrototype.vsCMPrototypeUniqueSignature Or EnvDTE.vsCMPrototype.vsCMPrototypeClassName)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Constructor_FullName()
            Dim code =
<Code>
class A
{
    public A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeFullname, "A.A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Finalizer_FullName()
            Dim code =
<Code>
class A
{
    ~A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeFullname, "A.~A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Operator_FullName()
            Dim code =
<Code>
class A
{
    public static A operator +$$(A a1, A a2)
    {
        return a1;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeFullname, "A.operator +")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Constructor_ReturnType()
            Dim code =
<Code>
class A
{
    public A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeType, "void A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Finalizer_ReturnType()
            Dim code =
<Code>
class A
{
    ~A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeType, "void ~A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Operator_ReturnType()
            Dim code =
<Code>
class A
{
    public static A operator +$$(A a1, A a2)
    {
        return a1;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeType, "A operator +")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Constructor_ClassName()
            Dim code =
<Code>
class A
{
    public A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "A.A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Finalizer_ClassName()
            Dim code =
<Code>
class A
{
    ~A$$()
    {
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "A.~A")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Prototype_Operator_ClassName()
            Dim code =
<Code>
class A
{
    public static A operator +$$(A a1, A a2)
    {
        return a1;
    }
}
</Code>

            TestPrototype(code, EnvDTE.vsCMPrototype.vsCMPrototypeClassName, "A.operator +")
        End Sub

#End Region

#Region "Type tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Type_Constructor()
            Dim code =
<Code>
class A
{
    public $$A()
    {
    }
}
</Code>

            TestTypeProp(code,
                New CodeTypeRefData With
                {
                    .AsFullName = "System.Void",
                    .AsString = "void",
                    .CodeTypeFullName = "System.Void",
                    .TypeKind = EnvDTE.vsCMTypeRef.vsCMTypeRefVoid
                })
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Type_Finalizer()
            Dim code =
<Code>
class A
{
    $$~A()
    {
    }
}
</Code>

            TestTypeProp(code,
                New CodeTypeRefData With
                {
                    .AsFullName = "System.Void",
                    .AsString = "void",
                    .CodeTypeFullName = "System.Void",
                    .TypeKind = EnvDTE.vsCMTypeRef.vsCMTypeRefVoid
                })
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Public Sub Type_Operator()
            Dim code =
<Code>
class A
{
    public static A operator +$$(A a1, A a2)
    {
        return a1;
    }
}
</Code>

            TestTypeProp(code,
                New CodeTypeRefData With
                {
                    .AsFullName = "A",
                    .AsString = "A",
                    .CodeTypeFullName = "A",
                    .TypeKind = EnvDTE.vsCMTypeRef.vsCMTypeRefCodeType
                })
        End Sub

#End Region

#Region "RemoveParameter tests"
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub RemoveParameter1()
            Dim code =
<Code>
class C
{
    void $$M(int a) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M() { }
}
</Code>

            TestRemoveChild(code, expected, "a")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub RemoveParameter2()
            Dim code =
<Code>
class C
{
    void $$M(int a, string b) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(int a) { }
}
</Code>

            TestRemoveChild(code, expected, "b")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub RemoveParameter3()
            Dim code =
<Code>
class C
{
    void $$M(int a, string b) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(string b) { }
}
</Code>

            TestRemoveChild(code, expected, "a")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub RemoveParameter4()
            Dim code =
<Code>
class C
{
    void $$M(int a, string b, int c) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(int a, int c) { }
}
</Code>

            TestRemoveChild(code, expected, "b")
        End Sub

#End Region

#Region "AddParameter tests"
        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub AddParameter1()
            Dim code =
<Code>
class C
{
    void $$M() { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(int a) { }
}
</Code>

            TestAddParameter(code, expected, New ParameterData With {.Name = "a", .Type = "int"})
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub AddParameter2()
            Dim code =
<Code>
class C
{
    void $$M(int a) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(string b, int a) { }
}
</Code>

            TestAddParameter(code, expected, New ParameterData With {.Name = "b", .Type = "string"})
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub AddParameter3()
            Dim code =
<Code>
class C
{
    void $$M(int a, string b) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(int a, bool c, string b) { }
}
</Code>

            TestAddParameter(code, expected, New ParameterData With {.Name = "c", .Type = "System.Boolean", .Position = 1})
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub AddParameter4()
            Dim code =
<Code>
class C
{
    void $$M(int a) { }
}
</Code>

            Dim expected =
<Code>
class C
{
    void M(int a, string b) { }
}
</Code>

            TestAddParameter(code, expected, New ParameterData With {.Name = "b", .Type = "string", .Position = -1})
        End Sub
#End Region

#Region "Set Access tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess1()
            Dim code =
<Code>
class C
{
    int $$Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    public int Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessPublic)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess2()
            Dim code =
<Code>
class C
{
    public int $$Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    internal int Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessProject)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess3()
            Dim code =
<Code>
class C
{
    protected internal int $$Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    public int Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessPublic)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess4()
            Dim code =
<Code>
class C
{
    public int $$Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    protected internal int Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessProjectOrProtected)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess5()
            Dim code =
<Code>
class C
{
    public int $$Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    int Foo()
    {
        throw new System.NotImplementedException();
    }
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessDefault)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess6()
            Dim code =
<Code>
interface I
{
    int $$Foo();
}
</Code>

            Dim expected =
<Code>
interface I
{
    int Foo();
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessProtected, ThrowsArgumentException(Of EnvDTE.vsCMAccess)())
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetAccess7()
            Dim code =
<Code>
interface I
{
    int $$Foo();
}
</Code>

            Dim expected =
<Code>
interface I
{
    int Foo();
}
</Code>

            TestSetAccess(code, expected, EnvDTE.vsCMAccess.vsCMAccessPublic)
        End Sub

#End Region

#Region "Set IsShared tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetIsShared1()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    static void Foo()
    {
    }
}
</Code>

            TestSetIsShared(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetIsShared2()
            Dim code =
<Code>
class C
{
    static void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    void Foo()
    {
    }
}
</Code>

            TestSetIsShared(code, expected, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetIsShared3()
            Dim code =
<Code>
class C
{
    $$C()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    static C()
    {
    }
}
</Code>

            TestSetIsShared(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetIsShared4()
            Dim code =
<Code>
class C
{
    static $$C()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    C()
    {
    }
}
</Code>

            TestSetIsShared(code, expected, False)
        End Sub

#End Region

#Region "Set CanOverride tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetCanOverride1()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    virtual void Foo()
    {
    }
}
</Code>

            TestSetCanOverride(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetCanOverride2()
            Dim code =
<Code>
class C
{
    virtual void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    virtual void Foo()
    {
    }
}
</Code>

            TestSetCanOverride(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetCanOverride3()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    void Foo()
    {
    }
}
</Code>

            TestSetCanOverride(code, expected, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetCanOverride4()
            Dim code =
<Code>
class C
{
    virtual void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    void Foo()
    {
    }
}
</Code>

            TestSetCanOverride(code, expected, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetCanOverride5()
            Dim code =
<Code>
interface I
{
    void $$Foo();
}
</Code>

            Dim expected =
<Code>
interface I
{
    void Foo();
}
</Code>

            TestSetCanOverride(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetCanOverride6()
            Dim code =
<Code>
interface I
{
    void $$Foo();
}
</Code>

            Dim expected =
<Code>
interface I
{
    void Foo();
}
</Code>

            TestSetCanOverride(code, expected, False, ThrowsArgumentException(Of Boolean))
        End Sub

#End Region

#Region "Set MustImplement tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement1()
            Dim code =
<Code>
abstract class C
{
    abstract void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
abstract class C
{
    abstract void Foo();
}
</Code>

            TestSetMustImplement(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement2()
            Dim code =
<Code>
abstract class C
{
    void $$Foo()
    {
        int i = 0;
    }
}
</Code>

            Dim expected =
<Code>
abstract class C
{
    abstract void Foo()
    {
        int i = 0;
    }
}
</Code>

            TestSetMustImplement(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement3()
            Dim code =
<Code>
abstract class C
{
    abstract void $$Foo();
}
</Code>

            Dim expected =
<Code>
abstract class C
{
    void Foo()
    {

    }
}
</Code>

            TestSetMustImplement(code, expected, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement4()
            Dim code =
<Code>
abstract class C
{
    abstract void $$Foo();
}
</Code>

            Dim expected =
<Code>
abstract class C
{
    abstract void Foo();
}
</Code>

            TestSetMustImplement(code, expected, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement5()
            Dim code =
<Code>
abstract class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
abstract class C
{
    void Foo()
    {
    }
}
</Code>

            TestSetMustImplement(code, expected, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement6()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    void Foo()
    {
    }
}
</Code>

            TestSetMustImplement(code, expected, True, ThrowsArgumentException(Of Boolean))
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement7()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    void Foo()
    {
    }
}
</Code>

            TestSetMustImplement(code, expected, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement8()
            Dim code =
<Code>
interface I
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
interface I
{
    void Foo()
    {
    }
}
</Code>

            TestSetMustImplement(code, expected, False, ThrowsArgumentException(Of Boolean))
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetMustImplement9()
            Dim code =
<Code>
interface I
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
interface I
{
    void Foo()
    {
    }
}
</Code>

            TestSetMustImplement(code, expected, True)
        End Sub

#End Region

#Region "Set OverrideKind tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetOverrideKind1()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    virtual void Foo()
    {
    }
}
</Code>

            TestSetOverrideKind(code, expected, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindVirtual)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetOverrideKind2()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    sealed void Foo()
    {
    }
}
</Code>

            TestSetOverrideKind(code, expected, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindSealed)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetOverrideKind3()
            Dim code =
<Code>
abstract class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
abstract class C
{
    abstract void Foo();
}
</Code>

            TestSetOverrideKind(code, expected, EnvDTE80.vsCMOverrideKind.vsCMOverrideKindAbstract)
        End Sub

#End Region

#Region "Set Name tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetName1()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    void Bar()
    {
    }
}
</Code>

            TestSetName(code, expected, "Bar", NoThrow(Of String)())
        End Sub

#End Region

#Region "Set Type tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub SetType1()
            Dim code =
<Code>
class C
{
    void $$Foo()
    {
    }
}
</Code>

            Dim expected =
<Code>
class C
{
    int Foo()
    {
    }
}
</Code>

            TestSetTypeProp(code, expected, "System.Int32")
        End Sub

#End Region

#Region "ExtensionMethodExtender"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub ExtensionMethodExtender_IsExtension1()
            Dim code =
<Code>
public static class C
{
    public static void $$Foo(this C c)
    {
    }
}
</Code>

            TestExtensionMethodExtender_IsExtension(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub ExtensionMethodExtender_IsExtension2()
            Dim code =
<Code>
public static class C
{
    public static void $$Foo(C c)
    {
    }
}
</Code>

            TestExtensionMethodExtender_IsExtension(code, False)
        End Sub

#End Region

#Region "PartialMethodExtender"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_IsPartial1()
            Dim code =
<Code>
public partial class C
{
    partial void $$M();
 
    partial void M()
    {
    }

    void M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_IsPartial(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_IsPartial2()
            Dim code =
<Code>
public partial class C
{
    partial void M();
 
    partial void $$M()
    {
    }

    void M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_IsPartial(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_IsPartial3()
            Dim code =
<Code>
public partial class C
{
    partial void M();
 
    partial void M()
    {
    }

    void $$M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_IsPartial(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_IsDeclaration1()
            Dim code =
<Code>
public partial class C
{
    partial void $$M();
 
    partial void M()
    {
    }

    void M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_IsDeclaration(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_IsDeclaration2()
            Dim code =
<Code>
public partial class C
{
    partial void M();
 
    partial void $$M()
    {
    }

    void M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_IsDeclaration(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_IsDeclaration3()
            Dim code =
<Code>
public partial class C
{
    partial void M();
 
    partial void M()
    {
    }

    void $$M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_IsDeclaration(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_HasOtherPart1()
            Dim code =
<Code>
public partial class C
{
    partial void $$M();
 
    partial void M()
    {
    }

    void M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_HasOtherPart(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_HasOtherPart2()
            Dim code =
<Code>
public partial class C
{
    partial void M();
 
    partial void $$M()
    {
    }

    void M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_HasOtherPart(code, True)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_HasOtherPart3()
            Dim code =
<Code>
public partial class C
{
    partial void M();
 
    partial void M()
    {
    }

    void $$M(int i)
    {
    }
}
</Code>

            TestPartialMethodExtender_HasOtherPart(code, False)
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub PartialMethodExtender_HasOtherPart4()
            Dim code =
<Code>
public partial class C
{
    partial void $$M();
}
</Code>

            TestPartialMethodExtender_HasOtherPart(code, False)
        End Sub

#End Region

#Region "Overloads Tests"

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub TestOverloads1()
            Dim code =
<Code>
public static class C
{
    public static void $$Foo()
    {
    }

    public static void Foo(C c)
    {
    }
}
</Code>
            TestOverloadsUniqueSignatures(code, "C.Foo()", "C.Foo(C)")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub TestOverloads2()
            Dim code =
<Code>
public static class C
{
    public static void $$Foo()
    {
    }
}
</Code>
            TestOverloadsUniqueSignatures(code, "C.Foo()")
        End Sub

        <ConditionalFact(GetType(x86)), Trait(Traits.Feature, Traits.Features.CodeModel)>
        Sub TestOverloads3()
            Dim code =
<Code>
class A
{
    public static A operator +$$(A a1, A a2)
    {
        return a1;
    }
}
</Code>
            TestOverloadsUniqueSignatures(code, "A.#op_Plus(A,A)")
        End Sub

#End Region

        Private Function GetExtensionMethodExtender(codeElement As EnvDTE80.CodeFunction2) As ICSExtensionMethodExtender
            Return CType(codeElement.Extender(ExtenderNames.ExtensionMethod), ICSExtensionMethodExtender)
        End Function

        Private Function GetPartialMethodExtender(codeElement As EnvDTE80.CodeFunction2) As ICSPartialMethodExtender
            Return CType(codeElement.Extender(ExtenderNames.PartialMethod), ICSPartialMethodExtender)
        End Function

        Protected Overrides Function ExtensionMethodExtender_GetIsExtension(codeElement As EnvDTE80.CodeFunction2) As Boolean
            Return GetExtensionMethodExtender(codeElement).IsExtension
        End Function

        Protected Overrides Function PartialMethodExtender_GetIsPartial(codeElement As EnvDTE80.CodeFunction2) As Boolean
            Return GetPartialMethodExtender(codeElement).IsPartial
        End Function

        Protected Overrides Function PartialMethodExtender_GetIsDeclaration(codeElement As EnvDTE80.CodeFunction2) As Boolean
            Return GetPartialMethodExtender(codeElement).IsDeclaration
        End Function

        Protected Overrides Function PartialMethodExtender_GetHasOtherPart(codeElement As EnvDTE80.CodeFunction2) As Boolean
            Return GetPartialMethodExtender(codeElement).HasOtherPart
        End Function

        Protected Overrides ReadOnly Property LanguageName As String
            Get
                Return LanguageNames.CSharp
            End Get
        End Property
    End Class
End Namespace

