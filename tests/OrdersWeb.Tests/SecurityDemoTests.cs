using Xunit;
using FluentAssertions;
using OrdersSecurity;
using OrdersWeb.SecurityDemos;

namespace OrdersWeb.Tests;

public class SecurityDemoTests
{
    [Fact]
    public void Pbkdf2_returns_salt_and_hash()
    {
        var result = PasswordHashing.HashPasswordPbkdf2("password123");
        result.Should().Contain(".");
        var parts = result.Split('.');
        parts.Length.Should().Be(2);
        parts[0].Length.Should().BeGreaterThan(10);
        parts[1].Length.Should().BeGreaterThan(10);
    }

    [Fact]
    public void Safe_json_deserialize_works_for_known_type()
    {
        var payload = "{ \"Message\": \"hello\" }";
        var dto = InsecureJson.DeserializeSafe<EchoDto>(payload);
        dto.Should().NotBeNull();
        dto!.Message.Should().Be("hello");
    }

    public record EchoDto(string Message);
}
