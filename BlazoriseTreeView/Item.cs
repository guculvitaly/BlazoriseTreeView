﻿using System.Collections.ObjectModel;
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


        public Item GetCheckedNodes(Item node)
        {
            Item parent = null;

            if (node.IsChecked)
            {
                parent = new Item
                {
                    NodeId = node.NodeId,
                    ParentId = node.ParentId,
                    IsChecked = node.IsChecked,
                    Text = node.Text,
                    Children = new List<Item>()
                };
            }

            if (node.Children != null)
            {
                foreach (Item child in node.Children)
                {
                    Item checkedChild = GetCheckedNodes(child);
                    if (checkedChild != null)
                    {
                        if (parent == null)
                        {
                            parent = new Item
                            {
                                NodeId = node.NodeId,
                                ParentId = node.ParentId,
                                IsChecked = node.IsChecked,
                                Text = node.Text,
                                Children = new List<Item>()
                            };
                        }

                        parent.Children.Add(checkedChild);
                    }
                }
            }

            return parent;
        }

        public Item FindItemByIdAndCheckParent(IEnumerable<Item> items, string parentId, string nodeId)
        {
            // Проходим по всем элементам в коллекции и ищем элемент с заданным идентификатором
            foreach (var item in items)
            {
                if (item.NodeId == parentId || item?.ParentId == nodeId)
                {
                    item.IsChecked = true;
                    return item;
                }

                // Если у элемента есть дочерние элементы, ищем в них
                else if (item.Children != null)
                {

                    var result = FindItemByIdAndCheckParent(item.Children, parentId, nodeId);

                    if (result != null)
                    {
                        result.IsChecked = true;

                        return result;
                    }
                }
            }

            return null;
        }

        public  Item FindChildById(Item node, string nodeId)
        {
            if (node.NodeId == nodeId)
            {
                return node;
            }
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var result = FindChildById(child, nodeId);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public  Tuple<Item, Item> FindNodeAndParent(List<Item> nodes, string nodeId)
        {
            foreach (var node in nodes)
            {
                var result = FindChildById(node, nodeId);
                if (result != null)
                {
                    return Tuple.Create(result, node);
                }
            }
            return null;
        }

    }



}
