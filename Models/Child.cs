using System.Collections.Generic;

namespace Models {
public class Child : Person {
    
    public List<ChildInterest> ChildInterests { get; set; }
    public List<Pet> Pets { get; set; }

    public Child(){
        ChildInterests = new List<ChildInterest>();
        Pets = new List<Pet>();
    }

    public void Update(Child toUpdate) {
        base.Update(toUpdate);
        ChildInterests = toUpdate.ChildInterests;
        Pets = toUpdate.Pets;
    }

}
}