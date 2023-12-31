﻿/*
Copyright © ArcToCore All Rights Reserved
Software license for CodeHammer
Summary
License does not expire.
Can be used for creating unlimited applications
Can be distributed in binary or object form only
Can modify source-code but cannot distribute modifications (derivative works)
 */

namespace CodeHammer.Crypto
{

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public abstract class CryptKey : DisposeableObject
    {
        CryptContext ctx;
        IntPtr handle;

        internal IntPtr Handle { get { return handle; } }

        internal CryptKey(CryptContext ctx, IntPtr handle)
        {
            this.ctx = ctx;
            this.handle = handle;
        }

        public abstract KeyType Type { get; }

        protected override void CleanUp(bool viaDispose)
        {
            // keys are invalid once CryptContext is closed,
            // so the only time I try to close an individual key is if a user
            // explicitly disposes of the key.
            if (viaDispose)
                ctx.DestroyKey(this);
        }
    }
}
