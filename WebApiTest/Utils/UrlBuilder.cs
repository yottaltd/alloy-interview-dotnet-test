using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace WebApiTest.Utils
{
    public class UrlBuilder
    {
        private readonly string _baseUrl;
        private readonly List<string> _pathSegments;
        private readonly Dictionary<string, List<object>> _queryParams;

        public UrlBuilder(string baseUrl)
        {
            _baseUrl = baseUrl;

            if (_baseUrl.EndsWith("/", StringComparison.InvariantCulture))
            {
                _baseUrl = _baseUrl.Substring(0, _baseUrl.Length - 1);
            }

            _pathSegments = new List<string>();
            _queryParams = new Dictionary<string, List<object>>();
        }

        public void AddPathSegment(string segment)
        {
            _pathSegments.Add(segment);
        }

        public void AppendQueryParam(string name, object value)
        {
            if (!_queryParams.ContainsKey(name))
            {
                _queryParams[name] = new List<object>();
            }

            if (value is IEnumerable values && !(values is string))
            {
                foreach (var item in values)
                {
                    _queryParams[name].Add(item);
                }
            }
            else
            {
                _queryParams[name].Add(value);
            }
        }

        public void AppendQueryParam(string name, IEnumerable<string> values)
        {
            if (!_queryParams.ContainsKey(name))
            {
                _queryParams[name] = new List<object>();
            }

            foreach (var value in values)
            {
                _queryParams[name].Add(value);
            }
        }

        public void SetQueryParam(string name, object value)
        {
            _queryParams[name] = new List<object> { value };
        }

        public void RemoveQueryParam(string name)
        {
            _queryParams.Remove(name);
        }

        public Uri ToUri()
        {
            var uriBuilder = new UriBuilder(_baseUrl);

            uriBuilder.Path = uriBuilder.Path + "/" + string.Join("/", _pathSegments);

            var queryParamPairStrings = new List<string>();
            foreach (var queryParam in _queryParams)
            {
                foreach (var value in queryParam.Value)
                {
                    var encodedValue = Uri.EscapeDataString(GetInvariantString(value ?? ""));
                    queryParamPairStrings.Add($"{queryParam.Key}={encodedValue}");
                }
            }

            uriBuilder.Query = string.Join("&", queryParamPairStrings);

            return uriBuilder.Uri;
        }

        public string GetInvariantString(object obj)
        {
            if (obj is IConvertible c)
            {
                return c.ToString(CultureInfo.InvariantCulture);
            }

            if (obj is IFormattable f)
            {
                return f.ToString(null, CultureInfo.InvariantCulture);
            }

            return obj.ToString();
        }
    }

}