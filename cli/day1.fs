﻿// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp
namespace AoC
module Day1 = 
    let rec binSearch target arr =
        match Array.length arr with
            | 0 -> None
            | i ->
                let middle = i / 2
                match  sign <| compare target arr.[middle] with
                    | 0  -> Some(target)
                    | -1 -> binSearch target arr.[..middle-1]
                    | _  -> binSearch target arr.[middle+1..]

                
    let rec findPair numbers sum =
        if Array.length numbers < 2 then
            None
        else
            let first = numbers.[0]
            if first * 2 > sum then
                None
            else
                let rest = numbers.[1..]
                let somePair = binSearch (sum - first) rest
                match somePair with
                    | None -> findPair rest sum
                    | Some(pair) -> Some(first, pair)

    let rec find2020Triple numbers =
        if Array.length numbers < 3 then
            None
        else
            let first = numbers.[0]
            if first > 674 then // 2020 / 3
                printfn "Bailing at %d, %A" first numbers
                None
            else
                let rest = numbers.[1..]
                let someTuple = findPair rest (2020 - first)
                match someTuple with
                    | None -> find2020Triple rest
                    | Some(second, third) -> Some(first, second, third)


    let handler part (lines : string seq) =
        let sortedNumbers = Seq.map int lines |> Seq.toArray |> Array.sort
        match part with
            | One ->
                let pair = findPair sortedNumbers 2020
                match pair with
                    | Some(a, b) -> sprintf "Pair: %d, %d -> %d" a b (a * b)
                    | None -> "Day 1.1 failed to find pair"
            | Two ->
                let triple = find2020Triple sortedNumbers
                match triple with
                    | Some(a, b, c) -> sprintf "Triple: %d, %d, %d -> %d" a b c (a * b * c)
                    | None -> "Day 1.2 failed to find triple"

  
             
