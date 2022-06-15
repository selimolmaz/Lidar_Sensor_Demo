using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace UnityEngine.XR.ARFoundation
{
    public class ConvertToJson : MonoBehaviour
    {
        
        ARMeshManager aRMeshManager;
    
        [SerializeField]
        GameObject manager;
        public CombineInstance[] combine;

        public IList<MeshFilter> myfilter=new List<MeshFilter>();
        
        public IList<Mesh> meshList=new List<Mesh>();



        void Awake(){

            aRMeshManager=manager.GetComponent<ARMeshManager>();
        }

        void Start(){
            logText.text="";
            myfilter=aRMeshManager.meshes;
            
            
        }
        
            

        [Tooltip("The UI Text element used to display log messages.")]
            [SerializeField]
            Text logText;

            /// <summary>
            /// The UI Text element used to display log messages.
            /// </summary>


                    

            public void SaveButtonWork(){
                
                MeshtoJson();
                
            }

            public void addToList(){
                myfilter=aRMeshManager.meshes;
                combine = new CombineInstance[myfilter.Count];
                for (int i = 0; i < myfilter.Count; i++)
                {
                    combine[i].mesh = myfilter[i].sharedMesh;
                    combine[i].transform = myfilter[i].transform.localToWorldMatrix;
                    
                }
                transform.GetComponent<MeshFilter>().mesh = new Mesh();
                transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
                transform.gameObject.SetActive(true);

                logText.text="eklendi";
            }
        public void MeshtoJson(){

            mymesh m=new mymesh();

            m.myvertices=transform.GetComponent<MeshFilter>().mesh.vertices;
            m.mytriangles=transform.GetComponent<MeshFilter>().mesh.triangles;
            m.mytangents=transform.GetComponent<MeshFilter>().mesh.tangents;
            m.mynormals=transform.GetComponent<MeshFilter>().mesh.normals;
            m.myUV=transform.GetComponent<MeshFilter>().mesh.uv;
            
            
            /*if(m.myvertices==null){
                m.myvertices=meshList[0].vertices;
            }
            
            if(m.myUV==null){
                m.myUV=meshList[0].uv;
            }
            
            
            if(m.mytriangles==null){
                m.mytriangles=meshList[0].triangles;
            }
            if(m.mynormals==null){
                m.mynormals=meshList[0].normals;
            }
            if(m.mytangents==null){
                m.mytangents=meshList[0].tangents;
            }

            if(meshList.Count>=1){
                for(int i=1;i<meshList.Count;i++){ //concat arrays
                
                    m.myvertices=m.myvertices.Concat(meshList[i].vertices).ToArray();

                    m.myUV=m.myUV.Concat(meshList[i].uv).ToArray();
                   

                    

                    
                    m.mytriangles=m.mytriangles.Concat(meshList[i].triangles).ToArray();

                    m.mynormals=m.mynormals.Concat(meshList[i].normals).ToArray();

                    m.mytangents=m.mytangents.Concat(meshList[i].tangents).ToArray();

                }
            }*/
            
            

            string json = JsonConvert.SerializeObject(m, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
            jsontotxt(json);
        }

        



        public  void jsontotxt(string json){
            
            string mypath=Path.Combine(Application.persistentDataPath, "myjson.txt");
            StreamWriter writer = new StreamWriter(mypath,false);
            writer.Write(json);
            writer.Close();
        
            using(WebClient client = new WebClient()) {
            client.UploadFile("http://10.10.10.37:5286/WeatherForecast", mypath);
            }

            logText.text="Server'a yükleme tamamlandı";

            
    }

    static void SetText(Text text, string value)
        {
                if (text != null)
                    text.text = value;
        }
    public class mymesh{
            public Vector3[] myvertices;

            public Vector2[] myUV;

            public int[] mytriangles;

            public Vector3[] mynormals;
            public Vector4[] mytangents;
            
            

        }
    }
}