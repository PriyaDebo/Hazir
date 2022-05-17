﻿using API.Models;
using Common.Models;

namespace API.Extensions
{
    public static class ClassExtension
    {
        public static ClassResponseModel ToAPIModel(this IClass iClass)
        {
            var model = new ClassResponseModel();
            model.Id = iClass.Id;
            model.TeacherId = iClass.TeacherId;
            model.StudentIds = iClass.StudentIds;
            model.CourseId = iClass.CourseId;
            foreach (var student in iClass.Students)
            {
                model.Students.Add(student.ToAPIModel());
            }
            return model;
        }
    }
}