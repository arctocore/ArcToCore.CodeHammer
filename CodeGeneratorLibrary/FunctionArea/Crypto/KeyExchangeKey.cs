/*
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


    public class KeyExchangeKey : CryptKey
    {
        internal KeyExchangeKey(CryptContext ctx, IntPtr handle) : base(ctx, handle)  {}
        
        public override KeyType Type
        {
            get { return KeyType.Exchange; }
        }
    }
}
