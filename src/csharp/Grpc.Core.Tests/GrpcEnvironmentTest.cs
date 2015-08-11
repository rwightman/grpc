#region Copyright notice and license

// Copyright 2015, Google Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are
// met:
//
//     * Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above
// copyright notice, this list of conditions and the following disclaimer
// in the documentation and/or other materials provided with the
// distribution.
//     * Neither the name of Google Inc. nor the names of its
// contributors may be used to endorse or promote products derived from
// this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
// OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Threading;
using Grpc.Core;
using NUnit.Framework;

namespace Grpc.Core.Tests
{
    public class GrpcEnvironmentTest
    {
        [Test]
        public void InitializeAndShutdownGrpcEnvironment()
        {
            var env = GrpcEnvironment.GetInstance();
            Assert.IsNotNull(env.CompletionQueue);
            GrpcEnvironment.Shutdown();
        }

        [Test]
        public void SubsequentInvocations()
        {
            var env1 = GrpcEnvironment.GetInstance();
            var env2 = GrpcEnvironment.GetInstance();
            Assert.IsTrue(object.ReferenceEquals(env1, env2));
            GrpcEnvironment.Shutdown();
            GrpcEnvironment.Shutdown();
        }

        [Test]
        public void InitializeAfterShutdown()
        {
            var env1 = GrpcEnvironment.GetInstance();
            GrpcEnvironment.Shutdown();

            var env2 = GrpcEnvironment.GetInstance();
            GrpcEnvironment.Shutdown();

            Assert.IsFalse(object.ReferenceEquals(env1, env2));
        }

        [Test]
        public void GetCoreVersionString()
        {
            var coreVersion = GrpcEnvironment.GetCoreVersionString();
            var parts = coreVersion.Split('.');
            Assert.AreEqual(4, parts.Length);
        }
    }
}