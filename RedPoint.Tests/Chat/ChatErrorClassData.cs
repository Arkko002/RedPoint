using System.Collections;
using System.Collections.Generic;
using NLog;
using RedPoint.Chat.Models.Errors;

namespace RedPoint.Tests.Chat
{
    public class ChatErrorClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {new ChatError(ChatErrorType.ServerNotFound, LogLevel.Fatal, "testMessage")};
            yield return new object[] {new ChatError(ChatErrorType.ChannelNotFound, LogLevel.Fatal, "testMessage")};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}