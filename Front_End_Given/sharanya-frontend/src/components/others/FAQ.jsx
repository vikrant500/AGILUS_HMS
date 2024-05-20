import React from "react";
import { styled } from "@mui/material/styles";
import ArrowForwardIosSharpIcon from "@mui/icons-material/ArrowForwardIosSharp";
import MuiAccordion from "@mui/material/Accordion";
import MuiAccordionSummary from "@mui/material/AccordionSummary";
import MuiAccordionDetails from "@mui/material/AccordionDetails";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
const Accordion = styled((props) => (
  <MuiAccordion disableGutters elevation={0} square {...props} />
))(({ theme }) => ({
  //   border: `1px solid ${theme.palette.divider}`,
  "&:before": {
    display: "none",
  },
}));

const AccordionSummary = styled((props) => (
  <MuiAccordionSummary
    expandIcon={<ArrowForwardIosSharpIcon sx={{ fontSize: "0.9rem" }} />}
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
  borderBottom: "1px solid rgba(0, 0, 0, .125)",
}));

const AccordionDetails = styled(MuiAccordionDetails)(({ theme }) => ({
  padding: theme.spacing(2),
  color: "gray",
}));

const FAQ = ({faq_list}) => {
  const [expanded, setExpanded] = React.useState("panel1");

  const handleChange = (panel) => (event, newExpanded) => {
    setExpanded(newExpanded ? panel : false);
  };


  return (
    <div className="px-[6%] py-16">
      <h3 className="font-bold text-2xl text-center">
        Frequently Asked Questions
      </h3>
      <div className="my-6">
        <Container>
        {faq_list.map((question) => (
          <React.Fragment key={question.question}>
            <Accordion
              expanded={expanded === question.question}
              onChange={handleChange(question.question)}
            >
              <AccordionSummary>
                <Typography>{question.question}</Typography>
              </AccordionSummary>
              <AccordionDetails>
                <Typography>{question.answer}</Typography>
              </AccordionDetails>
            </Accordion>
          </React.Fragment>
        ))}
        </Container>
      </div>
    </div>
  );
};

export default FAQ;
