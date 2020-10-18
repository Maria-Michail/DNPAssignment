using System.Collections.Generic;
using Models;

namespace Assignment.Data{
    public interface IGetFamilies{
        List<Adult> allAdults();
        List<Child> allChildren();
        IList<Family> allfamilies();
        void addNewAdult(Adult adult, int familyNumber);
        void addNewChild(Child child, int familyNumber);
        void addInterest(int childID, string interest);
        void addPetForFamily(Pet pet, int familyNumber);
        void addPetForChild(Pet pet, int childNumber);
        void addNewFamily(Family family);
}
}
