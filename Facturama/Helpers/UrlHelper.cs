using System;
using System.Collections.Generic;
using System.Web;
using Facturama.Models;

namespace Facturama.Helpers
{
    public static class UrlHelper
    {
        public static string BuildFullUrl(string baseUri, string path, Dictionary<string, string> queryParams = null)
        {
            if (string.IsNullOrEmpty(baseUri))
                throw new ArgumentException("Base URI cannot be null or empty", nameof(baseUri));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var builder = new UriBuilder(baseUri);
            return BuildFullUrl(builder, path, queryParams);
        }

        public static string BuildFullUrl(Uri baseUri, string path, Dictionary<string, string> queryParams = null)
        {
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var builder = new UriBuilder(baseUri);
            return BuildFullUrl(builder, path, queryParams);
        }
        public static string BuildFullUrl(UriBuilder uriBuilder, string path, Dictionary<string, string> queryParams = null)
        {
            if (uriBuilder == null)
                throw new ArgumentNullException(nameof(uriBuilder));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            var pathAndQuery=SeparatePathAndQuery(path);
            uriBuilder.Path = CombinePaths(uriBuilder.Path, pathAndQuery.Path);
            foreach (var item in pathAndQuery.QueryParams)
            {
                queryParams.Add(item.Key,item.Value);
            }
            if (queryParams != null && queryParams.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query ?? "");
                foreach (var param in queryParams)
                {
                    query[param.Key] = param.Value;
                }
                uriBuilder.Query = query.ToString();
            }

            return uriBuilder.Uri.ToString();
        }
        private static PathAndQuery SeparatePathAndQuery(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new PathAndQuery { Path=input, QueryParams=null};

            var questionMarkIndex = input.IndexOf('?');
            if (questionMarkIndex < 0)
                return new PathAndQuery { Path=input, QueryParams=null}; // No hay query parameters

            var path = input.Substring(0, questionMarkIndex);
            var queryString = input.Substring(questionMarkIndex + 1);

            var queryParams = ParseQueryString(queryString);

            return new PathAndQuery { Path = path, QueryParams = queryParams };
        }
        private static Dictionary<string, string> ParseQueryString(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return null;

            var queryParams = new Dictionary<string, string>();
            var pairs = queryString.Split('&');

            foreach (var pair in pairs)
            {
                var keyValue = pair.Split('=');
                if (keyValue.Length >= 1 && !string.IsNullOrEmpty(keyValue[0]))
                {
                    var key = Uri.UnescapeDataString(keyValue[0]);
                    var value = keyValue.Length >= 2 ? Uri.UnescapeDataString(keyValue[1]) : "";
                    queryParams[key] = value;
                }
            }

            return queryParams;
        }

        private static string CombinePaths(string basePath, string relativePath)
        {
            basePath = basePath?.Trim('/') ?? "";
            relativePath = relativePath.Trim('/');

            if (string.IsNullOrEmpty(basePath))
                return relativePath;

            if (string.IsNullOrEmpty(relativePath))
                return basePath;

            return $"{basePath}/{relativePath}";
        }
    }
}
