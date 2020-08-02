using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RedPoint.Chat.Services.Security;

namespace RedPoint.Tests.Chat
{
    public class ChatErrorClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new ChatError(ChatErrorType.ServerNotFound, LogLevel.Critical, "testMessage") };
            yield return new object[] { new ChatError(ChatErrorType.ChannelNotFound, LogLevel.Critical, "testMessage") };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}