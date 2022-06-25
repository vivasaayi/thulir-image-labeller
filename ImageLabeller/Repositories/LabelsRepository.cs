using Dapper;
using ImageLabeller.Dals;
using ImageLabeller.DbModels;
using ImageLabeller.WebModels;
using Npgsql;
using Label = ImageLabeller.DbModels.Label;

namespace ImageLabeller.Repositories;

public class LabelsRepository
{
    private PostgresDal _dal;

    public LabelsRepository()
    {
        _dal = PostgresDal.GetInstance();
        SqlMapper.AddTypeHandler(new GenericTypeHandler<Label>());
        SqlMapper.AddTypeHandler(new GenericTypeHandler<Point>());
    }

    public async Task<ImageLabel> GetLabels(Guid imageId)
    {
        try
        {
            string command = @"SELECT * FROM imagelabels where imageid=@imageid";

            var result = await _dal.ExecuteQuery<ImageLabel>(command, new
            {
                imageid = imageId
            });

            return result.ToList()[0];
        }
        catch (PostgresException err)
        {
            Console.WriteLine(err);
        }

        return new ImageLabel();
    }

    public async Task SaveLabels(Guid imageId, ImageLabeller.DbModels.ImageLabel label)
    {
        try
        {
            // string command = @"INSERT INTO imagelabels(imageid, labels, createddate, lastmodifieddate) 
            //                     values(@imageid, @labels, @createddate, @lastmodifieddate)";

            string command = @"INSERT INTO imagelabels (imageid, labels, createddate, lastmodifieddate) 
                                values(@imageid, @labels, @createddate, @lastmodifieddate)
                                ON CONFLICT (imageid) DO UPDATE 
                                  SET labels = excluded.labels, 
                                      lastmodifieddate = excluded.lastmodifieddate;";

            await _dal.InsertRecord(command, new
            {
                imageid = imageId,
                labels = label.Labels,
                createddate = DateTime.Now,
                lastmodifieddate = DateTime.Now
            });
        }
        catch (PostgresException err)
        {
            Console.WriteLine(err);
        }
    }
}