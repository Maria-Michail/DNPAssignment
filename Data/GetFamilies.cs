using System.Collections.Generic;
using Models;
using FileData;


namespace Assignment.Data{
    public class GetFamilies: IGetFamilies{
    private static FileContext fileContext = new FileContext();
    private IList<Family> families = fileContext.Families;
    public List<Adult> allAdults(){
        List<Adult> adults = new List<Adult>();
        foreach(var item in families){
            if(item.Adults !=null){
                foreach(var adul in item.Adults){
                adults.Add(adul);
                }
            }
        }
        return adults;
    }
    public List<Child> allChildren(){
        List<Child> children = new List<Child>();
        foreach(var item in families){
            if(item.Children !=null){
                foreach(var chil in item.Children){
                children.Add(chil);
                }
            }
        }
        return children;
    }
    public List<Pet> allPets(){
        List<Pet> pets = new List<Pet>();
        foreach(var item in families){
            if(item.Pets !=null){
                foreach(var pet in item.Pets){
                    pets.Add(pet);
                }
            }
            if(item.Children != null){
                foreach (var child in item.Children)
                {
                    if(child.Pets != null){
                        foreach(var pet in child.Pets){
                            pets.Add(pet);
                        }
                    }  
                }
            }
        }
        return pets;
    }
    public IList<Family> allfamilies(){
        return families;
    }
    public void addNewAdult(Adult newAdult, int familyNumber){
        if(isAlreadyAdult(newAdult)){
            foreach(Family family in families){
                foreach(Adult adult in family.Adults){
                    if(adult.Id == newAdult.Id){
                        adult.Update(newAdult);
                    }
                }
            }
        }
        else{
            if(familyNumber<families.Count){
                if(families[familyNumber].Adults.Count<2){
                    families[familyNumber].Adults.Add(newAdult);
                }
            }
        }
        fileContext.SaveChanges();
    }
    public void addNewChild(Child newChild, int familyNumber){
        if(isAlreadyChild(newChild)){
            foreach(Family family in families){
                foreach(Child child in family.Children){
                    if(child.Id == newChild.Id){
                        child.Update(newChild);
                    }
                }
            }
        }
        else{
            if(familyNumber<families.Count){
                if(families[familyNumber].Children.Count<7){
                    families[familyNumber].Children.Add(newChild);
                }
            }
        }
        fileContext.SaveChanges();
    }

    public void addInterest(int newChildID, string newInterest){
        if(newChildID<allChildren().Count){
            Child child = getChildBasedOnId(newChildID);
            if(!(isAlreadyInterest(child, newInterest))){
                foreach(Family family in families){
                    foreach(Child child1 in family.Children){
                        if(child1.Id == newChildID){
                            Interest interest = new Interest();
                            interest.Type = newInterest;
                            ChildInterest childInterest = new ChildInterest();
                            childInterest.Child = child1;
                            childInterest.Interest = interest;
                            childInterest.ChildId = child1.Id;
                            childInterest.InterestId = interest.Type;
                            child1.ChildInterests.Add(childInterest);
                        }
                    }
                }
            }
        }
        fileContext.SaveChanges();
    }

    public void addPetForFamily(Pet newPet, int familyNumber){
        if(isAlreadyPet(newPet)){
            foreach(Family family in families){
                foreach(Pet pet in family.Pets){
                    if(pet.Id == newPet.Id){
                        pet.Name = newPet.Name;
                        pet.Species = newPet.Species;
                        pet.Age = newPet.Age;
                    }
                }
            }
        }
        else{
            if(familyNumber<families.Count){
                    families[familyNumber].Pets.Add(newPet);
            }
        }
        fileContext.SaveChanges();
    }

    public void addPetForChild(Pet newPet, int childNumber){
        if(isAlreadyPet(newPet)){
            foreach(Family family in families){
                foreach(Child child in family.Children){
                    foreach(Pet pet in child.Pets){
                        if(pet.Id == newPet.Id){
                        pet.Name = newPet.Name;
                        pet.Species = newPet.Species;
                        pet.Age = newPet.Age;
                        }
                    }
                }
            }
        }
        else{
            if(childNumber<allChildren().Count){
                foreach(Family family in families){
                    foreach(Child child in family.Children){
                        if(child.Id == childNumber){
                            child.Pets.Add(newPet);
                        }
                    }
                }
            }
        }
        fileContext.SaveChanges();
    }

    public void addNewFamily(Family family){
        if(!isAlreadyFamily(family)){
            fileContext.Families.Add(family);
            fileContext.SaveChanges();
        }
    }

    public bool isAlreadyAdult(Adult newAdult){
        foreach(Adult adult in allAdults()){
            if(adult.Id == newAdult.Id){
                return true;
            }
        }
        return false;
    }
    public bool isAlreadyChild(Child newChild){
        foreach(Child child in allChildren()){
            if(child.Id == newChild.Id){
                return true;
            }
        }
        return false;
    }
    public bool isAlreadyFamily(Family family){
        foreach(Family family1 in families){
            if(family1.StreetName.Equals(family.StreetName) && family1.HouseNumber == family.HouseNumber){
                return true;
            }
        }
        return false;
    }
    public bool isAlreadyPet(Pet pet){
        foreach(Pet animal in allPets()){
            if(animal.Id == pet.Id){
                return true;
            }
        }
        return false;
    }
    public Child getChildBasedOnId(int childID){
        foreach(Family family in families){
            foreach(Child child in family.Children){
                if(child.Id == childID){
                    return child;
                }
            }
        }
        return null;
    }
    public bool isAlreadyInterest(Child child, string interest){
        foreach(ChildInterest childInterest in child.ChildInterests){
            if(childInterest.InterestId.Equals(interest)){
                return true;
            }
        }
        return false;
    }
}
}
