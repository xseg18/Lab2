using Lab2.Models.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Parte_1;
using Lab3;

namespace Lab2.Controllers
{
    [ApiController]
    [Route("api")]
    public class HuffmanController : Controller
    {
        public static IWebHostEnvironment environment;
        public HuffmanController(IWebHostEnvironment _environment)
        {
            environment = _environment;
        }
        [Route("{method}/compress/{name}")]
        [HttpPost]
        public ActionResult Compress([FromRoute] string method, [FromRoute] string name, IFormFile file)
        {
            if (method.ToUpper() == "LZW")
            {
                try
                {
                    if (file.Length > 0)
                    {
                        if (!Directory.Exists(environment.WebRootPath + "\\Upload\\"))
                        {
                            Directory.CreateDirectory(environment.WebRootPath + "\\Upload\\");
                        }
                        using (FileStream stream = new FileStream(environment.WebRootPath + "\\Upload\\" + file.FileName, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        byte[] readBytes = System.IO.File.ReadAllBytes(environment.WebRootPath + "\\Upload\\" + file.FileName);
                        ILZW lzw = new ImplementationClassL();
                        byte[] compressedBytes = lzw.Compress(readBytes);
                        var newCompression = new Compressions
                        {
                            originalName = file.FileName,
                            compressedName = name + ".lzw",
                            path = Environment.CurrentDirectory,
                            method = "LZW",
                            ratio = Convert.ToDouble(compressedBytes.Length) / Convert.ToDouble(readBytes.Length),
                            factor = Convert.ToDouble(readBytes.Length) / Convert.ToDouble(compressedBytes.Length),
                            percentage = Convert.ToString(Convert.ToDouble(compressedBytes.Length) / Convert.ToDouble(readBytes.Length) * 100) + "%"
                        };
                        System.IO.File.WriteAllBytes(name + ".lzw", readBytes);
                        Singleton.Instance.Compressions.Add(newCompression);
                        return Ok("Archivo comprimido en: " + Environment.CurrentDirectory);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            else if (method.ToUpper() == "HUFFMAN")
            {
                try
                {
                    if (file.Length > 0)
                    {
                        var fileBytes = (dynamic)null;
                        var compressedBytes = (dynamic)null;
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        IHuffman h = new ImplementationClass();
                        string compressed = h.Comprimir(Encoding.UTF8.GetString(fileBytes));
                        double fileCount = Convert.ToDouble(Encoding.UTF8.GetByteCount(Encoding.UTF8.GetString(fileBytes)));
                        double compressedCount = Convert.ToDouble(Encoding.UTF8.GetByteCount(compressed));
                        compressedBytes = Encoding.UTF8.GetBytes(file.FileName + compressed);
                        var newCompression = new Compressions
                        {
                            originalName = file.FileName,
                            compressedName = name + ".huff",
                            path = Environment.CurrentDirectory,
                            method = "Huffman",
                            ratio = compressedCount / fileCount,
                            factor = fileCount / compressedCount,
                            percentage = Convert.ToString((compressedCount / fileCount) * 100) + "%"
                        };
                        System.IO.File.WriteAllBytes(name + ".huff", compressedBytes);
                        Singleton.Instance.Compressions.Add(newCompression);
                        return Ok("Archivo comprimido en: " + Environment.CurrentDirectory);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("{method}/decompress")]
        [HttpPost]
        public IActionResult Decompress([FromRoute] string method, IFormFile file)
        {
            if (method.ToUpper() == "LZW")
            {
                try
                {
                    if (file.Length > 0)
                    {
                        if (!Directory.Exists(environment.WebRootPath + "\\Upload\\"))
                        {
                            Directory.CreateDirectory(environment.WebRootPath + "\\Upload\\");
                        }
                        using (FileStream stream = new FileStream(environment.WebRootPath + "\\Upload\\" + file.FileName, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        ILZW lzw = new ImplementationClassL();
                        int found = file.FileName.IndexOf(".lzw");
                        System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + file.FileName.Substring(0, found) + "_Descomprimido" + ".txt", lzw.Decompress(System.IO.File.ReadAllBytes(environment.WebRootPath + "\\Upload\\" + file.FileName)));
                        return Ok("Archivo descomprimido en: " + Environment.CurrentDirectory);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            else if (method.ToUpper() == "HUFFMAN")
            {
                try
                {
                    if (file.Length > 0)
                    {
                        var fileBytes = (dynamic)null;
                        var compressedBytes = (dynamic)null;
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        IHuffman h = new ImplementationClass();
                        int found = Encoding.UTF8.GetString(fileBytes).IndexOf(".txt");
                        compressedBytes = Encoding.UTF8.GetBytes(h.Descomprimir(Encoding.UTF8.GetString(fileBytes).Substring(found + 4)));
                        System.IO.File.WriteAllBytes(Encoding.UTF8.GetString(fileBytes).Substring(0, found + 4), compressedBytes);
                        return Ok("Archivo descomprimido en : " + Environment.CurrentDirectory);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("compressions")]
        [HttpGet]
        public ActionResult Compressions()
        {
            return Ok(JsonSerializer.Serialize(Singleton.Instance.Compressions));
        }
    }
}
