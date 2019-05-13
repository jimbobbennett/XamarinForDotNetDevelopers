// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace MKFabulous

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms

module App = 
    type Model = 
         { 
             Count : int
         }

    type Msg = 
        | Increment 

    let initModel = { Count = 0; }

    let init () = initModel, Cmd.none
    
    let update msg model =
        match msg with
        | Increment -> { model with Count = model.Count + 1 }, Cmd.none
       
    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
            children = [ 
                View.Label(text = sprintf "%d" model.Count, horizontalOptions = LayoutOptions.Center, widthRequest=200.0, horizontalTextAlignment=TextAlignment.Center)
                View.Button(text = "Increment", command = (fun () -> dispatch Increment), horizontalOptions = LayoutOptions.Center)
            ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> Program.runWithDynamicView app

#if DEBUG
    do runner.EnableLiveUpdate()
#endif    

