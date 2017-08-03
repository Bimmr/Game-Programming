package com.bimmr.assignment5;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Created by Randy on 12/04/16.
 * Class for the topbar
 */
public class TopBar extends JPanel {

    private final MainFrame mainFrame;
    private JTextField rows, cols;


    /**
     * Create a new TopBar
     * @param assignment5 The main class
     */
    public TopBar(MainFrame assignment5) {
        this.mainFrame = assignment5;

        this.setLayout(new FlowLayout(0));

        rows = new JTextField("20", 5);
        cols = new JTextField("20", 5);

        this.add(new JLabel("Rows: "));
        this.add(rows);
        this.add(new JLabel("Cols: "));
        this.add(cols);

        JButton btnGenerate = new JButton("Generate");
        JButton btnSave = new JButton("Save");
        JButton btnLoad = new JButton("Load");

        ButtonHandler handler = new ButtonHandler();
        btnGenerate.addActionListener(handler);
        btnSave.addActionListener(handler);
        btnLoad.addActionListener(handler);

        this.add(btnGenerate);
        this.add(btnSave);
        this.add(btnLoad);
        this.setBorder(BorderFactory.createEtchedBorder(0));
    }

    /**
     * Button Handler for the TopBar
     */
    private class ButtonHandler implements ActionListener {

        /**
         * Click Event
         * @param e The Event
         */
        @Override
        public void actionPerformed(ActionEvent e) {

            String nameOfButton = ((JButton) e.getSource()).getText();
            switch (nameOfButton) {

                //Generate has been clicked
                case "Generate":
                    try {

                        //Get rows/cols
                        int rowNumber = Integer.parseInt(rows.getText());
                        int colNumber = Integer.parseInt(cols.getText());

                        //Create the needed boxes
                        mainFrame.getMainContent().createBoxes(rowNumber, colNumber, null);

                    } catch (NumberFormatException ex) {
                        JOptionPane.showMessageDialog(mainFrame, "Rows/Cols need to be a number larger than 0");
                    }
                    break;

                //Save has been clicked
                case "Save":
                    if (mainFrame.getMainContent() != null)
                        try {

                            //Get rows & col
                            int row = mainFrame.getMainContent().getRow();
                            int col = mainFrame.getMainContent().getCol();

                            //Create a string[] of the color's RGB values
                            String[] colors = new String[row * col];
                            Box[] boxes = mainFrame.getMainContent().getBoxes();

                            for (int i = 0; i < boxes.length; i++)
                                colors[i] = "" + boxes[i].getBackground().getRGB();

                            //Save the array to a file
                            MainFrame.save(new Object[]{row, col, colors});
                        } catch (Exception e1) {
                            e1.printStackTrace();
                            JOptionPane.showMessageDialog(mainFrame, "Something went wrong while saving");
                        }
                    break;

                //Load has been clicked
                case "Load":
                    try {

                        //Load the conent
                        Object[] data = mainFrame.load();
                        if(data != null) {

                            //Init the attributes
                            int row = (int) data[0];
                            int col = (int) data[1];

                            //Get the colors
                            String[] cols = (String[]) data[2];
                            Color[] color = new Color[row * col];
                            for (int i = 0; i < color.length; i++)
                                color[i] = new Color(Integer.parseInt(cols[i]));

                            //Set the boxes for the main content
                            mainFrame.getMainContent().createBoxes(row, col, color);
                        }
                    } catch (Exception e1) {
                        e1.printStackTrace();
                        JOptionPane.showMessageDialog(mainFrame, "Something went wrong while loading");
                    }
                    break;
            }
        }
    }
}
