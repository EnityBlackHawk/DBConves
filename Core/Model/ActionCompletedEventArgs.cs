using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ActionCompletedEventArgs
    {

        public object? ArtifactObject { get; }
        public Type? ArtifactType { get; }
        public Result Result { get; }


        public ActionCompletedEventArgs(Result result, object? artifactObject = null, Type? artifactType = null)
        {
            ArtifactObject = artifactObject;
            ArtifactType = artifactType;
            Result = result;
        }
    }
}
