using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ImageLabeller.Dals;
using ImageLabeller.DbModels;
using ImageLabeller.Models;
using ImageLabeller.Repositories;
using ImageLabeller.Utilities;
using Microsoft.AspNetCore.Http.Connections;
using NUnit.Framework;

namespace ImageLabellerUnitTest.ImageRepository;


public class LabelsRepositoryTest
{
    [SetUp]
    public async Task Setup()
    {
        ImageLabellerGlobals globals = await ConfigLoader.GetInstance().GetGlobals();
        PostgresDal.Init(new PostgresConfig(
            globals.PostgresHost,
            globals.PostgresUserName,
            globals.PostgresPassword,
            globals.PostgresDatabase)
        );
    }

    [Test]
    public async Task Test1()
    {
        LabelsRepository _labelsRepository = new LabelsRepository();

        var guid = Guid.NewGuid();

        var label1 = new Label()
        {
            Id = 123,
            LabelName = "ABC",
            Points = new List<Point>()
            {
                new Point() {X = 111, Y = 222}
            }
        };
        
        var label = new ImageLabel()
        {
            Labels = new List<Label>()
            {
                label1
            }
        };
        
        await _labelsRepository.SaveLabels(guid, label);
        var result = await _labelsRepository.GetLabels(guid);
        
        Assert.AreEqual(result.Labels[0].Id, 123);
        Assert.AreEqual(result.Labels[0].LabelName, "ABC");
        Assert.AreEqual(result.Labels[0].Points[0].X, 111);
        Assert.AreEqual(result.Labels[0].Points[0].Y, 222);
        
        
        await _labelsRepository.SaveLabels(guid, label);
        result = await _labelsRepository.GetLabels(guid);
        
        Assert.AreEqual(result.Labels[0].Id, 123);
        Assert.AreEqual(result.Labels[0].LabelName, "ABC");
        Assert.AreEqual(result.Labels[0].Points[0].X, 111);
        Assert.AreEqual(result.Labels[0].Points[0].Y, 222);
        
        Assert.Pass();
    }
}