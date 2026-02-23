using Demo_PrimaryConstructor;

Demo01 obj = new Demo01( 10, "first object" );
// obj.id = 50;                             // would work if data-field was not marked private
// obj.name = obj.Name.ToUpper();           // would work if property was not marked private


Demo02 obj2 = new Demo02(20, "second object");
// obj2.id                                  // will NOT work since PRIMARY CONSTRUCTOR marks parameters as PRIVATE

