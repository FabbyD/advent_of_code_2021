package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
)

func main() {
	octopuses := readOctopuses()
	flashCount := 0

	print(octopuses)
	for i := 1; i <= 100; i++ {
		flashCount += step(octopuses)
		fmt.Printf("Step %v : %v\n", i, flashCount)
		print(octopuses)
	}

}

func step(octopuses [][]int) int {
	for i := 0; i < len(octopuses); i++ {
		row := octopuses[i]
		for j := 0; j < len(row); j++ {
			row[j]++
		}
	}

	flashCount := 0
	for i := 0; i < len(octopuses); i++ {
		row := octopuses[i]
		for j := 0; j < len(row); j++ {
			if row[j] > 9 {
				flashCount += flash(octopuses, i, j)
			}
		}
	}

	return flashCount
}

func flash(octopuses [][]int, x, y int) int {
	octopuses[x][y] = 0
	flashCount := 1

	for i := x - 1; i <= x+1; i++ {
		// outside the grid
		if i < 0 || i >= len(octopuses) {
			continue
		}

		row := octopuses[i]
		for j := y - 1; j <= y+1; j++ {
			// outside the grid
			if j < 0 || j >= len(row) {
				continue
			}

			// if it's 0, it already flashed this step
			if octopuses[i][j] == 0 {
				continue
			}

			octopuses[i][j]++
			if octopuses[i][j] > 9 {
				flashCount += flash(octopuses, i, j)
			}
		}
	}

	return flashCount
}

func print(octopuses [][]int) {
	for _, s := range octopuses {
		fmt.Println(s)
	}
}

func readOctopuses() [][]int {
	file, err := os.Open("../inputs/day11.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	octopuses := make([][]int, 10)
	row := 0
	for scanner.Scan() {
		text := scanner.Text()
		octopuses[row] = make([]int, len(text))
		for column, r := range text {
			n := int(r - '0')
			octopuses[row][column] = n
		}
		row++
	}

	if err := scanner.Err(); err != nil {
		log.Fatal(err)
	}

	return octopuses
}
