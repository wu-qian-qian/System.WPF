using System.Windows;

namespace DynamicTheme.Core
{
    public class ThemeManager
    {
        Dictionary<string, ResourceDictionary> _themes = new();

        /// <summary>
        /// 本地程序集
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="resourcePath"></param>
        public void RegisterTheme(string themeName, string resourcePath)
        {
            ResourceDictionary resource = new ResourceDictionary();
            resource.Source = new Uri(resourcePath, UriKind.RelativeOrAbsolute);
            _themes.Add(themeName, resource);
        }
        /// <summary>
        /// 其他程序集注册方式
        /// </summary>
        /// <param name="themeName"></param>
        /// <param name="assemblyName"></param>
        /// <param name="resourcePath"></param>
        public void RegisterTheme(string themeName, string assemblyName, string resourcePath)
        {
            string uri = $"/{assemblyName};component/{resourcePath}";
            ResourceDictionary resource = new ResourceDictionary();
            resource.Source = new Uri(uri, UriKind.RelativeOrAbsolute);
            _themes.Add(themeName, resource);
        }

        public void ApplyTheme(string themeName)
        {
            ResourceDictionary theme = _themes[themeName];

            foreach (var kvp in _themes)
            {
                Application.Current.Resources.MergedDictionaries.Remove(kvp.Value);
            }

            Application.Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}
