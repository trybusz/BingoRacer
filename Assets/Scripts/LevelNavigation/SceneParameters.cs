    using System.Collections.Generic;

    public static class SceneContext {
        private static Dictionary<string, string> context = new();

        public static Dictionary<string, string> Get() {
            return context;
        }

        public static string GetElement(string key) {
            return context.ContainsKey(key) ? context[key] : null;
        }

        public static void Set(Dictionary<string, string> newContext) {
            if (newContext == null) {
                context.Clear();
            } else {
                context = newContext;
            }
        }

        public static void SetElement(string key, string value) {
            if (context.ContainsKey(key)) {
                context[key] = value;
            } else {
                context.Add(key, value);
            }
        }

        public static void Clear() {
            context.Clear();
        }

        public static void ClearElement(string key) {
            context.Remove(key);
        }
    }