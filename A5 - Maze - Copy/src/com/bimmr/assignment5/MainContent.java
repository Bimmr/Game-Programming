package com.bimmr.assignment5;

import javax.swing.*;
import java.awt.*;
import java.io.Serializable;

/**
 * Created by Randy on 12/04/16.
 *
 * The MainContent
 */
public class MainContent extends JPanel implements Serializable {

    private MainFrame mainFrame;
    private int row, col;
    private Box[] boxes;

    /**
     * Create a default MainContent
     * @param mainFrame - the project's main class
     */
    public MainContent(MainFrame mainFrame) {
        this.mainFrame = mainFrame;
    }

    /**
     * Get the boxes
     * @return All Boxes in the main content
     */
    public Box[] getBoxes() {
        return boxes;
    }

    /**
     * Create new boxes for the main content
     * @param row Amount of rows
     * @param col Amount of columns
     * @param colors The List of colors
     */
    public void createBoxes(int row, int col, Color[] colors) {
        if(boxes != null)
            for(Box box : boxes)
                remove(box);

        //Create the layout with all the boxes
        setLayout(new GridLayout(row, col));
        this.row = row;
        this.col = col;
        boxes = new Box[row * col];

        for (int i = 0; i < row * col; i++) {
            Box box = new Box(mainFrame);

            if (colors != null && colors[i] != null)
                box.setColor(colors[i]);

            boxes[i] = box;
            add(box);
        }

        //Refresh the container
        revalidate();
        repaint();
    }

    /**
     * Get the rows
     * @return Rows
     */
    public int getCol() {
        return col;
    }

    /**
     * Get the cols
     * @return Cols
     */
    public int getRow() {
        return row;
    }
}
