package com.bimmr.assignment5;

import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.PrintWriter;
import java.lang.reflect.Field;

/**
 * Created by Randy on 12/04/16.
 * The main window that gets shown
 */
public class MainFrame extends JFrame {

    public static MainFrame instance;
    private String action;
    private MainContent mainContent;

    /***
     * Set everything up that is needed
     */
    public MainFrame() {

        instance = this;
        //Set attributes
        setTitle("RBAssignment 5");
        setSize(new Dimension(800, 500));
        setLocationRelativeTo(null);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(new TopBar(this), BorderLayout.NORTH);
        add(new SideBar(this), BorderLayout.WEST);
        add(mainContent = new MainContent(this), BorderLayout.CENTER);

        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (ClassNotFoundException | InstantiationException | UnsupportedLookAndFeelException | IllegalAccessException e) {
            e.printStackTrace();
        }

        setAction("Clear");

        //Show JFrame and content
        setVisible(true);
    }

    /**
     * Main method
     * @param args run time args
     */
    public static void main(String[] args) {

        new MainFrame();
    }

    /**
     * Get the color by the name using reflection
     *
     * Example: "GREEN" will return Color.GREEN
     * @param colorName Color Name
     * @return Color
     * @throws Exception
     */
    public static Color getColorByName(String colorName) throws Exception {
        Field field = Color.class.getField(colorName);
        return (Color) field.get(null);

    }

    /**
     * Load contents from a file
     * @return a list of [rows,cols,colors]
     * @throws Exception
     */
    public static Object[] load() throws Exception {
        JFileChooser fc = new JFileChooser();
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Text", "txt");
        fc.setFileFilter(filter);
        int r = fc.showOpenDialog(instance);
        if (r == 0) {
            FileReader read = new FileReader(fc.getSelectedFile());
            BufferedReader br = new BufferedReader(read);

            int row = Integer.parseInt(br.readLine());
            int col = Integer.parseInt(br.readLine());
            String[] colors = br.readLine().split(",");

            br.close();
            read.close();

            return new Object[]{row, col, colors};
        }
        return null;

    }

    /**
     * Save the objects to the file
     * [row,col,colors]
     * @param objects The objects
     * @throws Exception
     */
    public static void save(Object[] objects) throws Exception {
        JFileChooser fc = new JFileChooser();
        FileNameExtensionFilter filter = new FileNameExtensionFilter("Text", "txt");
        fc.setFileFilter(filter);
        int r = fc.showSaveDialog(instance);
        if (r == 0) {
            PrintWriter fw = new PrintWriter(fc.getSelectedFile().getPath() + ".txt");

            String[] array = (String[]) objects[2];
            String line = "";

            for (String c : array)
                line += c + ",";
            line.substring(0, line.length() - 2);

            fw.write(objects[0] + "\n");
            fw.write(objects[1] + "\n");
            fw.write(line);

            fw.close();
            JOptionPane.showMessageDialog(instance, "File has been saved");

        }
    }

    /**
     * Get the current action(Color to set)
     * @return
     */
    public String getAction() {
        return action;
    }

    /**
     * Set the current action(Color to set)
     * @param action
     */
    public void setAction(String action) {
        this.action = action;
    }

    /**
     * Get the main content panel
     * @return Main Content Panel
     */
    public MainContent getMainContent() {
        return mainContent;
    }
}
