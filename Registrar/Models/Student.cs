namespace Registrar.Models
{
  public class Student
  {
    public Student()
    {
      this.JoinEntities = new Hashset<CourseStudent>();
    }

    public int StudentId { get; set; }
    
  }
}