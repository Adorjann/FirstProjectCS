using FirstProjectCS.exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FirstProjectCS
{
    public class CustomException 
    {
        private CustomException()
        {

        }

        public ObjectNotFoundException GetObjectNotFoundException()
        {
            return new ObjectNotFoundException("The object was not found, [Check your input].");
        }

        public CollectionIsEmptyException GetCollectionIsEmptyException()
        {
            return new CollectionIsEmptyException("Empty Collection Found.");
        }

        public DuplicateObjectException GetDuplicateObjectException()
        {
            return new DuplicateObjectException();
        }


    }
    

}
