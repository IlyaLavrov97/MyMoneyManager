using MyMoneyManager.Infrastucture.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyManager.Infrastucture
{
    public class Interlayer
    {
        private List<IDtoObject> elementsFromDb = new List<IDtoObject>();
        private List<IDtoObject> elementsFromClient = new List<IDtoObject>();

        public void SplitNewElements(List<IDtoObject> newElements, out List<IDtoObject> addedElements, out List<IDtoObject> changedElements, out List<IDtoObject> deletedElements)
        {
            elementsFromClient = newElements;

            addedElements = new List<IDtoObject>();
            changedElements = new List<IDtoObject>();
            deletedElements = new List<IDtoObject>();

            bool isOldEl = false;

            List<Guid> lstClientIds = new List<Guid>();

            foreach (var item in elementsFromClient)
            {
                lstClientIds.Add(item.GetId());
            }

            deletedElements = (from dbElement in elementsFromDb
                               where !lstClientIds.Contains(dbElement.GetId())
                               select dbElement).ToList();

            foreach (var clientElement in elementsFromClient)
            {
                foreach (var dbElement in elementsFromDb)
                {
                    if (dbElement.GetId() == clientElement.GetId())
                    {
                        if(!dbElement.IsEqual(clientElement))
                        {
                            changedElements.Add(clientElement);
                        }
                        isOldEl = true;
                        break;
                    }
                }
                if (!isOldEl)
                {
                    addedElements.Add(clientElement);
                }
                isOldEl = false;
            }

            elementsFromDb = elementsFromClient;
        }

        public void SetElementsFromDb(List<IDtoObject> lst)
        {
            elementsFromDb = lst;
        }
    }
}
