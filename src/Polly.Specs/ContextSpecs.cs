﻿using System;
using FluentAssertions;
using Polly.Specs.Helpers;
using Xunit;

namespace Polly.Specs;

public class ContextSpecs
{
    [Fact]
    public void Should_assign_OperationKey_from_constructor()
    {
        var context = new Context("SomeKey");

        context.OperationKey.Should().Be("SomeKey");

        context.Keys.Count.Should().Be(0);
    }

    [Fact]
    public void Should_assign_OperationKey_and_context_data_from_constructor()
    {
        var context = new Context("SomeKey", new { key1 = "value1", key2 = "value2" }.AsDictionary());

        context.OperationKey.Should().Be("SomeKey");
        context["key1"].Should().Be("value1");
        context["key2"].Should().Be("value2");
    }

    [Fact]
    public void NoArgsCtor_should_assign_no_OperationKey()
    {
        var context = new Context();

        context.OperationKey.Should().BeNull();
    }

    [Fact]
    public void Should_assign_CorrelationId_when_accessed()
    {
        var context = new Context("SomeKey");

        context.CorrelationId.Should().NotBeEmpty();
    }

    [Fact]
    public void Should_return_consistent_CorrelationId()
    {
        var context = new Context("SomeKey");

        var retrieved1 = context.CorrelationId;
        var retrieved2 = context.CorrelationId;

        retrieved1.Should().Be(retrieved2);
    }
}