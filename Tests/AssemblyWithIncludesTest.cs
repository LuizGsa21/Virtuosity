﻿using NUnit.Framework;

[TestFixture]
public class AssemblyWithIncludesTest
{
    [Test]
    public void Simple()
    {
        var weaverHelper = new WeaverHelper(@"AssemblyWithIncludes\AssemblyWithIncludes.csproj", "IncludeNamespace", null);
        var assembly = weaverHelper.Assembly;
        var excludeType = assembly.GetType("ExcludeNamespace.ExcludeClass");
        Assert.IsFalse(excludeType.GetMethod("Method").IsVirtual);
        var includeType = assembly.GetType("IncludeNamespace.IncludeClass");
        Assert.IsTrue(includeType.GetMethod("Method").IsVirtual);

        var inNamespaceButWithAttributeType = assembly.GetType("IncludeNamespace.InNamespaceButWithAttributeClass");
        Assert.IsFalse(inNamespaceButWithAttributeType.GetMethod("Method").IsVirtual);
        var notInNamespaceButWithAttributeType = assembly.GetType("ExcludeNamespace.NotInNamespaceButWithAttributeClass");
        Assert.IsFalse(notInNamespaceButWithAttributeType.GetMethod("Method").IsVirtual);
    }
}