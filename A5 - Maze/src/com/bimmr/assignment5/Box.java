package com.bimmr.assignment5;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

/**
 * Created by Randy on 12/04/16.
 *
 * Default Box Tile for the main content
 */
public class Box extends JPanel implements MouseListener {

    private final MainFrame mainFrame;

    /**
     *
     Create a new Box
     */
    public Box(MainFrame mainFrame) {
        this.mainFrame = mainFrame;

        setBorder(BorderFactory.createEtchedBorder(0));
        addMouseListener(this);

    }

    /**
     * Set the color for the box
     * @param color - The Color
     * @return The Box object
     */
    public Box setColor(Color color) {
        setBackground(color);
        return this;
    }

    /**
     * On Mouse Click set the color accordingly
     * @param e - The event
     */
    @Override
    public void mouseClicked(MouseEvent e) {
        String action = mainFrame.getAction().toUpperCase();
        if (action.equals("CLEAR"))
            setColor(null);
        else
            try {
                setColor(MainFrame.getColorByName(action));
            } catch (Exception e1) {

            }

    }

    @Override
    public void mousePressed(MouseEvent e) {

    }

    @Override
    public void mouseReleased(MouseEvent e) {

    }

    @Override
    public void mouseEntered(MouseEvent e) {

    }

    @Override
    public void mouseExited(MouseEvent e) {

    }

}
