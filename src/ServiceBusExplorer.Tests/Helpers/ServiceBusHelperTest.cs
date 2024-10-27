#region Using Directives

using System;
using Microsoft.ServiceBus.Messaging;
using System.IO;
using FluentAssertions;
using Xunit;

#endregion

namespace ServiceBusExplorer.Tests.Helpers
{
    public class ServiceBusHelperTest
    {
        [Fact]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_ReturnsRelativePath()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("sb://aaa.test.com/some/path/segments/name").Should().Be("some/path/segments/name");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespaceTrailingSlash_ReturnsRelativePathWithTrailingSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("sb://aaa.test.com/some/path/segments/name/").Should().Be("some/path/segments/name/");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_Different_Scheme_ReturnsRelativePath()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("http://aaa.test.com/some/path/segments/name").Should().Be("some/path/segments/name");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_Different_Scheme_Port_Specified_ReturnsRelativePath()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com:80/")
            };

            helper.GetAddressRelativeToNamespace("http://aaa.test.com:80/some/path/segments/name").Should().Be("some/path/segments/name");
        }

        [Theory]
        [InlineData(":80", ":81")]
        [InlineData(":80", "")]
        [InlineData("", ":80")]
        public void GetAddressRelativeToNamespace_AbsoluteUriUnderNamespace_Different_Scheme_Different_Port_ReturnsLastSegment(string namespacePort, string port)
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri($"sb://aaa.test.com{namespacePort}/")
            };

            helper.GetAddressRelativeToNamespace($"http://aaa.test.com{port}/some/path/segments/name").Should().Be("some/path/segments/name");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_AbsoluteUriNotUnderNamespace_ReturnsLastSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("sb://bbb.test.com/some/path/segments/name").Should().Be("name");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_AbsoluteUriNotUnderNamespaceTrailingSlash_ReturnsLastSegmentWithTrailingSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("sb://bbb.test.com/some/path/segments/name/").Should().Be("name/");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_RelativeUriWithMultipleSegments_ReturnsLastSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("some/path/segments/name").Should().Be("name");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_RelativeUriWithMultipleSegmentsTrailingSlash_ReturnsLastSegmentWithTrailingSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("some/path/segments/name/").Should().Be("name/");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_RelativeUriWithSingleSegment_ReturnsSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("name").Should().Be("name");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_RelativeUriWithSingleSegmentAndTrailingSlash_ReturnsSegment()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("name/").Should().Be("name/");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_Slash_ReturnsSlash()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("/").Should().Be("/");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_Empty_ReturnsEmpty()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("").Should().Be("");
        }

        [Fact]
        public void GetAddressRelativeToNamespace_Whitespace_ReturnsWhitespace()
        {
            var helper = new ServiceBusHelper((m, a) => { })
            {
                NamespaceUri = new Uri("sb://aaa.test.com/")
            };

            helper.GetAddressRelativeToNamespace("    ").Should().Be("    ");
        }

        [Fact]
        public void GetMessageText_ProtobufMessage_WindowsAzureServiceApi_ByteArray()
        {
            var expectedBodyType = BodyType.ByteArray;

            var helper = new ServiceBusHelper((_, _) => { });

            var message = CreateProtobufTestMessage();
            var brokeredMessage = new BrokeredMessage(message);
            var actualMessageText = helper.GetMessageText(brokeredMessage, false, out var actualBodyType);

            expectedBodyType.Should().Be(actualBodyType);
            actualMessageText.Should().NotBeNull();
            actualMessageText.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetMessageText_ProtobufMessage_WindowsAzureServiceApi_ReadonlyMemoryByte()
        {
            var expectedBodyType = BodyType.Stream;

            var helper = new ServiceBusHelper((_, _) => { });

            ReadOnlyMemory<byte> message = CreateProtobufTestMessage();
            var brokeredMessage = new BrokeredMessage(message);
            var actualMessageText = helper.GetMessageText(brokeredMessage, false, out var actualBodyType);

            expectedBodyType.Should().Be(actualBodyType);
            actualMessageText.Should().NotBeNull();
            actualMessageText.Should().NotBeNullOrWhiteSpace();
        }

        /// <summary>
        /// AzureMessagingServiceBus only allows byte[] and BinaryData as content of the message.
        /// This test somehow try to emulate the behavior of the new nuget package.
        /// The exception is the same unspecified exception that occurs in test with the real service bus
        /// Inside of the package you would do something like
        /// Body = BinaryData.FromBytes(myByteArray)
        ///
        /// The actual message text in real test are like the output of <see cref="GetMessageText_ProtobufMessage_WindowsAzureServiceApi_ByteArray"/>
        /// 
        /// ������&
        /// a string that a human can read
        /// readable string"		fffff�(@
        /// </summary>
        [Fact]
        public void GetMessageText_ProtobufMessage_AzureMessagingServiceBus()
        {
            var expectedBodyType = BodyType.Stream;

            var helper = new ServiceBusHelper((_, _) => { });

            var message = CreateProtobufTestMessage();
            var brokeredMessage = new BrokeredMessage(new MemoryStream(message));
            var actualMessageText = helper.GetMessageText(brokeredMessage, false, out var actualBodyType);

            expectedBodyType.Should().Be(actualBodyType);
            actualMessageText.Should().NotBeNull();
            actualMessageText.Should().NotBeNullOrWhiteSpace();
        }

        static byte[] CreateProtobufTestMessage()
        {
            // Due to incompatibility with .net 462 it is not possible to generate a protobuf message inside the code.
            return
            [
                10,
                12,
                8,
                154,
                239,
                224,
                160,
                6,
                16,
                172,
                239,
                162,
                196,
                1,
                18,
                38,
                10,
                30,
                97,
                32,
                115,
                116,
                114,
                105,
                110,
                103,
                32,
                116,
                104,
                97,
                116,
                32,
                97,
                32,
                104,
                117,
                109,
                97,
                110,
                32,
                99,
                97,
                110,
                32,
                114,
                101,
                97,
                100,
                16,
                1,
                26,
                2,
                8,
                12,
                18,
                30,
                10,
                15,
                114,
                101,
                97,
                100,
                97,
                98,
                108,
                101,
                32,
                115,
                116,
                114,
                105,
                110,
                103,
                16,
                2,
                34,
                9,
                9,
                102,
                102,
                102,
                102,
                102,
                230,
                40,
                64
            ];
        }
    }
}
