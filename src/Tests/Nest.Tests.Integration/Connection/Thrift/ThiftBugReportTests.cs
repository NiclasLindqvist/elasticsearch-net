﻿using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;
using System.Net;

namespace Nest.Tests.Integration.Core.Bulk
{
	[TestFixture]
	public class ThiftBugReportTests : IntegrationTests
	{
		[Test]
		public void IndexExistShouldNotThrowOn404()
		{
			var isValidThriftConnection = this._thriftClient.RootNodeInfo().IsValid;
			isValidThriftConnection.Should().BeTrue();

			var unknownIndexResult = this._thriftClient.IndexExists(i=>i.Index("i-am-running-out-of-clever-index-names"));
			unknownIndexResult.Should().NotBeNull();
			unknownIndexResult.IsValid.Should().BeTrue();

			unknownIndexResult.Exists.Should().BeFalse();

			unknownIndexResult.ConnectionStatus.HttpStatusCode.Should().Be(404);

		}
	}
}
