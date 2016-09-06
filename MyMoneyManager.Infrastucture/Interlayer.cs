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
        private List<DtoObject> elementsFromDb = new List<DtoObject>();
        private List<DtoObject> elementsFromClient = new List<DtoObject>();

        public void SplitNewElements(List<DtoObject> newElements, out List<DtoObject> addedElements, out List<DtoObject> changedElements, out List<DtoObject> deletedElements)
        {
            elementsFromClient = newElements;

            addedElements = new List<DtoObject>();
            changedElements = new List<DtoObject>();
            deletedElements = new List<DtoObject>();

            bool isOldEl = false;

            List<Guid> lstClientIds = new List<Guid>();

            foreach (var item in elementsFromClient)
            {
                lstClientIds.Add(item.Id);
            }

            deletedElements = (from dbElement in elementsFromDb
                               where !lstClientIds.Contains(dbElement.Id)
                               select dbElement).ToList();

            foreach (var clientElement in elementsFromClient)
            {
                foreach (var dbElement in elementsFromDb)
                {
                    if (dbElement.Id == clientElement.Id)
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

        public void SetElementsFromDb(List<DtoObject> lst)
        {
            elementsFromDb = lst;
        }
    }
}
