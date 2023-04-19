using System.Collections.ObjectModel;
using System.Text.Json;

namespace BlazoriseTreeView
{
    public class Item
    {
        public Item()
        {

        }
        
        public string? ParentId { get; set; }
        public string NodeId { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Caret up or down
        /// </summary>
        public bool ShowItem { get; set; }
        public bool IsChecked { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsInteracted => Children?.Count() > 0 ? true : false;
        public string CaretClass => AddOrRemoveCaretClass();
        public string ShowOrHideUl => !ShowItem ? "hideChild" : string.Empty;

        public List<Item> Children { get; set; }
        public List<string> CheckedNodes = new List<string>();
        public Item Parent { get; set; }

        private string AddOrRemoveCaretClass()
        {
            if (ShowItem)
            {
                return "fas fa-angle-down";
            }
            if (Children == null || !Children.Any())
            {
                return string.Empty;
            }
            else
            {
                return "fas fa-angle-right";
            }

        }




        public List<Item> BuildList(IEnumerable<Item> items, Item parent = null)
        {
            var result = new List<Item>();

            foreach (var item in items)
            {
                if (item.ParentId == null && parent == null || item.ParentId == parent?.NodeId)
                {
                    var newItem = new Item
                    {
                        Text = item.Text,
                        NodeId = item.NodeId,
                        Parent = parent,
                        Children = BuildList(items, item)
                    };
                    result.Add(newItem);
                }
            }

            return result;
        }

        public Item FindItemById(IEnumerable<Item> items, string id)
        {
            // Проходим по всем элементам в коллекции и ищем элемент с заданным идентификатором
            foreach (var item in items)
            {
                if (item.NodeId == id)
                {
                    item.IsChecked = true;
                    return item;
                }

                // Если у элемента есть дочерние элементы, ищем в них
                else if (item.Children != null)
                {
                    var result = FindItemById(item.Children, id);

                    if (result != null)
                    {
                        result.IsChecked = true;
                        return result;
                    }
                }
            }

            return null;
        }
        public Item AddItemToCollection(IEnumerable<Item> Items, Item item)
        {

            if (item == null)
                return null;

            // Проверяем, выбран ли элемент
            if (item.IsChecked)
            {
                // Находим родительский элемент в дереве
                Item parent = FindItemById(Items, item.ParentId);
                if (parent == null)
                {
                    // Если не нашли родительский элемент, добавляем текущий элемент в корень коллекции
                    
                    return new Item
                    {
                        NodeId = item.NodeId,
                        Text = item.Text,
                        Parent = item.Parent,
                        ParentId = item.ParentId,
                        IsChecked = item.IsChecked,
                        Children = item.Children,
                    };
                }
                else
                {
                    // Если нашли родительский элемент, добавляем текущий элемент в коллекцию его дочерних элементов
                    if (parent.Children == null)
                        

                    parent.Children.Add(item);
                    parent.IsExpanded = true; // Разворачиваем родительский элемент, чтобы показать добавленный элемент
                    return parent;
                }
            }

            // Рекурсивно вызываем метод для каждого дочернего элемента текущего элемента
            if (item.Children != null)
            {
                foreach (Item child in item.Children)
                {
                    AddItemToCollection(Items, child);
                }
            }

            return null;
        }
        


    }



}
