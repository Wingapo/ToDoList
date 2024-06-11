using System.Xml;
using ToDoApp.Models;

namespace ToDoApp.Data.Services
{
    public class CategoryServiceXml : ICategoryService
    {
        private readonly XmlContext _context;
        private readonly XmlNode _categoryRoot;

        public CategoryServiceXml(XmlContext context)
        {
            _context = context;
            _categoryRoot = _context.Document.SelectSingleNode("data/Categories")!;
        }

        public int Add(Category category)
        {
            XmlNode lastCategoryId = _context.Document.SelectSingleNode("data/LastId/Category")!;

            int id = int.Parse(lastCategoryId.InnerText) + 1;
            category.Id = id;

            XmlElement categoryXml = _context.Serialize(category)!;
            XmlNode node = _categoryRoot.OwnerDocument!.ImportNode(categoryXml, true);

            _categoryRoot.AppendChild(node);
            lastCategoryId.InnerText = id.ToString();

            _context.Save();
            return id;
        }

        public void Delete(int id)
        {
            XmlNode? categoryXml = _categoryRoot.SelectSingleNode($"//Category[Id/text() = \"{id}\"]");

            if (categoryXml != null)
            {
                _categoryRoot.RemoveChild(categoryXml);
            }

            string query = $"data/Notes/Note/Categories/Id[text() = \"{id}\"]";
            XmlNodeList idsXml = _context.Document.SelectNodes(query)!;

            foreach (XmlNode idXml in idsXml)
            {
                idXml.ParentNode!.RemoveChild(idXml);
            }

            _context.Save();
        }

        public Category? Get(int id)
        {
            XmlNode? categoryXml = _categoryRoot.SelectSingleNode($"//Category[Id/text() = \"{id}\"]");

            return (categoryXml != null)
                ? _context.Deserialize<Category>(categoryXml)
                : null;
        }

        public IEnumerable<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            foreach (XmlNode categoryXml in _categoryRoot.ChildNodes)
            {
                Category? category = _context.Deserialize<Category>(categoryXml);

                if (category != null)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }
    }
}
