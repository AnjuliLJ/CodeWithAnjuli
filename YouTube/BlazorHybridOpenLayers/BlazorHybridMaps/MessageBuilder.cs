using Microsoft.JSInterop;

namespace BlazorHybridMaps
{
    public class MessageBuilder
    {
        private string message { get; set; } = string.Empty;

        public MessageBuilder(string message)
        {
            this.message = message;
        }

        [JSInvokable]
        public Task<string> getStringMessage() { return Task.FromResult(message); }
    }
}
