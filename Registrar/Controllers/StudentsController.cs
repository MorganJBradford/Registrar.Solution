using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic; // to be able to use lists, dictionary
using System.Linq;// ORM, to be able query objects
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering; //html helpers

namespace Registrar.Controllers
{
  public class StudentsController : Controllers
  {
    private readonly RegistrarContext _db;
    public StudentsController(RegistrarContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      return View(_db.Students.ToList());
    }

    public ActionResult Create()
    {
      Viewbag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student, int CourseId)
    {
      _db.Students.Add(student);
      _db.SaveChanges();
      if (CourseId != 0)
      {
        _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Student thisStudent = _db.Students
        .Include(student => student.JoinEntities)
        .ThenInclude(join => join.Course)
        . FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    } 
  }
}