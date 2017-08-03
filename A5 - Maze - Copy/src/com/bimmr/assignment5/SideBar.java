package com.bimmr.assignment5;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Created by Randy on 12/04/16.
 * Class for the topbar
 */
public class SideBar extends JPanel {


    private final MainFrame mainFrame;

    /**
     * Create a sidebar
     * @param mainFrame Main class
     */
    public SideBar(MainFrame mainFrame) {

        this.mainFrame = mainFrame;
        setLayout(new GridLayout(10, 1));
        ButtonHandler handler = new ButtonHandler();

        //Add Buttons
        JButton[] btns = new JButton[]{
                new JButton("Clear"),
                new JButton("Red"),
                new JButton("Blue"),
                new JButton("Green"),
                new JButton("Yellow"),
                new JButton("Pink"),
                new JButton("Cyan"),
                new JButton("Magenta"),
                new JButton("White"),
                new JButton("Black")};

        for (JButton btn : btns) {
            btn.addActionListener(handler);
            add(btn);
        }


        setBorder(BorderFactory.createEtchedBorder(0));
    }

    /**
     * Button Handler for the SideBar
     */
    private class ButtonHandler implements ActionListener {

        /**
         * Click Event
         * @param e The Event
         */
        @Override
        public void actionPerformed(ActionEvent e) {
            String action = ((JButton) e.getSource()).getText();
            mainFrame.setAction(action);
        }
    }
}
