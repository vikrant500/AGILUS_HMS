import React from "react";
import { styled } from "@mui/material/styles";
import ArrowForwardIosSharpIcon from "@mui/icons-material/ArrowForwardIosSharp";
import MuiAccordion from "@mui/material/Accordion";
import MuiAccordionSummary from "@mui/material/AccordionSummary";
import MuiAccordionDetails from "@mui/material/AccordionDetails";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import HelpOutlineIcon from '@mui/icons-material/HelpOutline';

const Accordion = styled((props) => (
  <MuiAccordion disableGutters elevation={0} square {...props} />
))(({ theme }) => ({
  "&:before": {
    display: "none",
  },
  backgroundColor: "#fff",
  boxShadow: "0 4px 8px rgba(0, 0, 0, 0.1)",
  borderRadius: "8px",
  marginBottom: theme.spacing(2),
  transition: "transform 0.3s ease, box-shadow 0.3s ease",
  "&:hover": {
    transform: "scale(1.02)",
    boxShadow: "0 6px 12px rgba(0, 0, 0, 0.2)",
  },
}));

const AccordionSummary = styled((props) => (
  <MuiAccordionSummary
    expandIcon={<ArrowForwardIosSharpIcon sx={{ fontSize: "0.9rem", color: "#6BB2A0" }} />}
    {...props}
  />
))(({ theme }) => ({
  flexDirection: "row-reverse",
  "& .MuiAccordionSummary-expandIconWrapper.Mui-expanded": {
    transform: "rotate(90deg)",
  },
  "& .MuiAccordionSummary-content": {
    marginLeft: theme.spacing(1),
  },
  padding: theme.spacing(1.5),
  borderBottom: "1px solid rgba(0, 0, 0, .125)",
  transition: "background-color 0.3s ease",
  "&:hover": {
    backgroundColor: "#CDEDC9",
  },
}));

const AccordionDetails = styled(MuiAccordionDetails)(({ theme }) => ({
  padding: theme.spacing(2),
  color: "gray",
}));

const FAQ = ({ faq_list }) => {
  const [expanded, setExpanded] = React.useState("panel1");

  const handleChange = (panel) => (event, newExpanded) => {
    setExpanded(newExpanded ? panel : false);
  };

  return (
    <div className="bg-gradient-to-r from-[#2C6975] via-[#6BB2A0] to-[#CDEDC9] px-[6%] py-16">
      <h3 className="font-bold text-3xl text-center text-[#EDECEDE] mb-8">
        Frequently Asked Questions
      </h3>
      <div className="my-6">
        <Container>
          {faq_list.map((question, index) => (
            <Accordion
              key={question.question}
              expanded={expanded === `panel${index}`}
              onChange={handleChange(`panel${index}`)}
            >
              <AccordionSummary>
                <HelpOutlineIcon sx={{ color: "#2C6975", marginRight: "8px" }} />
                <Typography className="font-semibold text-lg text-[#2C6975]">
                  {question.question}
                </Typography>
              </AccordionSummary>
              <AccordionDetails>
                <Typography className="text-gray-700">
                  {question.answer}
                </Typography>
              </AccordionDetails>
            </Accordion>
          ))}
        </Container>
      </div>
    </div>
  );
};

export default FAQ;
