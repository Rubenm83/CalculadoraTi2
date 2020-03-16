﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Calculadora.Models;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string operando;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Representa a view com a calculadora no primeiro pedido - HTTP GET
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Visor = 0;
            ViewBag.LimparVisor = "false";

            return View();
        }

        /// 
        /// <summary>
        /// Representa a view com a calculadora - HTTP POST
        /// </summary>
        /// <param name="visor">Mostra o valo do visot</param>
        /// <param name="bt">identifica o valor da tecla clicada</param>       
        /// <param name="operando">guarda o valor do primeiro operando para utilização na operação</param>   
        /// <param name="operador">operador a utilizar na operação</param>   
        /// <param name="limparVisor">indentifica se o visor de ser , ou não , limpo</param>   
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(string visor, string bt, string operando, string operador,bool limparVisor)
        {

            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":

                    if (visor == "0" || limparVisor == true) visor = bt;
                    else visor += bt; //visor = visor + bt;

                    //impedir o visor de ser limpo
                    limparVisor = false;
                    break;

                case "+/-":
                    //inverter o valor do visor 
                    //pode ser feito de duas formas
                    // - multiplicar por -1 => converter o valor do Visor para número
                    // - processar a string : visor.StartsWith().ToString().Substring().Length

                    visor = Convert.ToDouble(visor) * -1 + "";
                    break;

                case ",":
                    if (!visor.Contains(",")) visor += bt;
                        break;


                case "+":

                case "-":

                case ":":

                case "x":

                    //primeira vez que um operador foi selecionado

                    if (operador != null) 
                    {
                        //executa a operação

                        //vars. auxiliares
                        double operando1 = Convert.ToDouble(operando);
                        double operando2 = Convert.ToDouble(visor);
                        switch (operador)                         
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                break;
                        }

                    }

                    //guardar valores para "memoria futura"
                    if (bt != "=") operador = bt;
                    else operador = "";
                    operando = visor;
                    limparVisor = true;

                    break;

                case "C":
                    visor = "0";
                    operador = "";
                    operando = "";
                    limparVisor = true;
                    break;


            }


            //Levar o resultado do 'visor' para a view 

            ViewBag.Visor = visor;
            ViewBag.Operador = operador;
            ViewBag.Operando = operando;
            ViewBag.LimparVisor = limparVisor + "";
            return View();
        }















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
