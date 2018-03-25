﻿using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    public class QuerySegmentParameter : KeyValueParameter<string, string>
    {
        private Func<string, string> _pathTransformer;
        private Func<string, bool> _isMetPathTransformer;

        public QuerySegmentParameter(Func<string, bool> isMetPathTransformer = null)
        {
            _isMetPathTransformer = isMetPathTransformer;
        }

        public QuerySegmentParameter(Func<string, string> pathTransformer = null)
        {
            _pathTransformer = pathTransformer;
        }

        public override bool IsMet(Session session)
        {
            var uri = new UriBuilder(session.fullUrl);

            return _isMetPathTransformer == null ? true : _isMetPathTransformer(uri.Query);
        }

        public override Session Transform(Session session)
        {
            var uri = new UriBuilder(session.fullUrl);

            if(string.IsNullOrEmpty(uri.Query))
            {
                return session;
            }

            if (_pathTransformer != null)
            {
                uri.Query = _pathTransformer(uri.Query.Substring(1));
            }

            session.fullUrl = uri.Uri.AbsoluteUri;

            return session;
        }
    }
}
