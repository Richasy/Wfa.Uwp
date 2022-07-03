// Copyright (c) Richasy. All rights reserved.

using FluentAssertions;
using Richasy.Toolkit;
using System;
using Xunit;

namespace Richasy.Toolkit.UnitTests
{
    public class ToolkitTests
    {
        [Fact]
        public void Test_Me()
        {
            false.Should().Be(true);
        }
    }
}
